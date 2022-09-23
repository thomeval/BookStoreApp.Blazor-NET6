using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<List<AuthorGetAllDto>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
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
        public async Task<ActionResult<AuthorGetSingleDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning("Unable to find Author with ID: " + id);
                    return NotFound();
                }

                var result = _mapper.Map<AuthorGetSingleDto>(author);
                return result;
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDto);
                author.Id = id;
                _context.Entry(author).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!await AuthorExists(id))
                {
                    return NotFound();
                }

                _logger.LogError(ex, $"Error Performing PUT in {nameof(PutAuthor)}");
                return this.ServerError();
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {

            try
            {
                var author = _mapper.Map<Author>(authorDto);

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("Unable to find Author with ID: " + id);
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteAuthor)}");
                return this.ServerError();
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await (_context.Authors.AnyAsync(e => e.Id == id));
        }
    }
}
