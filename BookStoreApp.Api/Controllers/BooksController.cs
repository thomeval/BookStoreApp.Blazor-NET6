using System.IO;
using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Book;
using System.Threading.Tasks;
using BookStoreApp.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace BookStoreApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(200)]
[ProducesResponseType(500)]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _repository;
    private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(IBooksRepository repository, IMapper mapper, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<List<BookGetAllDto>>> GetBooks()
        {
            try
            {
                var books = await _repository.GetAllBooksAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetBooks)}");
                return this.ServerError();
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BookGetSingleDto>> GetBook(int id)
        {
            try
            {
                var book = await _repository.GetBookAsync(id);
                
                if (book == null)
                {
                    _logger.LogWarning("Unable to find Book with ID: " + id);
                    return NotFound();
                }

                var result = _mapper.Map<BookGetSingleDto>(book);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetBook)}");
                return this.ServerError();
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            
                var existingBook = await _repository.GetAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                if (!string.IsNullOrEmpty(bookDto.ImageData))
                {
                    bookDto.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                    DeleteFile(existingBook.Image);
                }
            
                _mapper.Map(bookDto, existingBook);

                try
                {
                    await _repository.UpdateAsync(existingBook);
                }
            catch (Exception ex)
            {
                if (!await _repository.Exists(id))
                {
                    return NotFound();
                }

                _logger.LogError(ex, $"Error Performing PUT in {nameof(PutBook)}");
                return this.ServerError();
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {

            try
            {
                var book = _mapper.Map<Book>(bookDto);
                book.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);

                await _repository.AddAsync(book);

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing POST in {nameof(PostBook)}");
                return this.ServerError();
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _repository.GetAsync(id);
                if (book == null)
                {
                    _logger.LogWarning("Unable to find Book with ID: " + id);
                    return NotFound();
                }

                await _repository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteBook)}");
                return this.ServerError();

            }
        }


        private string CreateFile(string imgBase64, string imgName)
        {
            var url = HttpContext.Request.Host.Value;
      
            var ext = Path.GetExtension(imgName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = GetFullPath(fileName);

            var imgContent = Convert.FromBase64String(imgBase64);
            System.IO.File.WriteAllBytes(path, imgContent);

            return $"{fileName}";
        }

        private string GetFullPath(string fileName)
        {
            return $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{fileName}";
        }

        private void DeleteFile(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var path = GetFullPath(fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
}
