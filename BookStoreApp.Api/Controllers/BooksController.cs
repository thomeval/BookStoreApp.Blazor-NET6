using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Book;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace BookStoreApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
      private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BooksController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<BookGetAllDto>>> GetBooks()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.Author)
                    .ProjectTo<BookGetAllDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BookGetSingleDto>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books
                    .Include(b => b.Author)
                    .ProjectTo<BookGetSingleDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(e => e.Id == id);

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
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto BookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(BookDto);
                book.Id = id;
                _context.Entry(book).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!await BookExists(id))
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
        [ProducesResponseType(500)]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto BookDto)
        {

            try
            {
                var book = _mapper.Map<Book>(BookDto);

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

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
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning("Unable to find Book with ID: " + id);
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteBook)}");
                return this.ServerError();
            }
        }

        private async Task<bool> BookExists(int id)
        {
            return await (_context.Books.AnyAsync(e => e.Id == id));
        }
}
