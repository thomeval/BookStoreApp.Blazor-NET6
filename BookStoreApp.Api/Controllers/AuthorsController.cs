using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;
using BookStoreApp.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorsRepository repository, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors/?startIndex=0&pageSize=15
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<AuthorGetAllDto>>> GetAuthors([FromQuery] QueryParameters queryParams)
        {
            try
            {
                var authors = await _repository.GetAllAsync<AuthorGetAllDto>(queryParams);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetAuthors)}");
                return this.ServerError();
            }
        }

        // GET: api/Authors/getAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<AuthorGetAllDto>>> GetAuthors()
        {
            try
            {
                var authors = await _repository.GetAllAsync();
                var result = _mapper.Map<List<AuthorGetAllDto>>(authors);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetAuthors)}");
                return this.ServerError();
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _repository.GetAuthorDetailsAsync(id);
                    
                if (author == null)
                {
                    _logger.LogWarning("Unable to find Author with ID: " + id);
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing GET in {nameof(GetAuthor)}");
                return this.ServerError();
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            try
            {
                var author = await _repository.GetAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PutAuthor)} - ID : {id}");
                    return NotFound();
                }

                _mapper.Map(authorDto, author);
                await _repository.UpdateAsync(author);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error Performing PUT in {nameof(PutAuthor)}");
                return this.ServerError();
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {

            try
            {
                var author = _mapper.Map<Author>(authorDto);

                await _repository.AddAsync(author);

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing POST in {nameof(PostAuthor)}");
                return this.ServerError();
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var exists = await _repository.Exists(id);
                if (!exists)
                {
                    _logger.LogWarning("Unable to find Author with ID: " + id);
                    return NotFound();
                }

                await _repository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteAuthor)}");
                return this.ServerError();
            }
        }

    }
}
