using AspNetCoreWebApi6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace AspNetCoreWebApi6.Controllers
{
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _dbContext;

        public MoviesController(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            return await _dbContext.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("Genre/{genre}")]
        public async Task<ActionResult<Movie>> GetMovieByGenre(string genre)
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.Where(m => m.Genre.Contains(genre)).ToArrayAsync();

            if (movie == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetMovieByGenre", movie);
        }

        [HttpGet("Title/{title}")]
        public async Task<ActionResult<Movie>> GetMovieByTitle(string title)
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.Where(m => m.Title.Contains(title)).ToArrayAsync();

            if (movie == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetMovieByTitle", movie);
        }

        private async Task AddMovie(Movie movie)
        {
            movie.Id = 0;
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
        }

        // PUT: api/Movies/5
        [HttpPut()]
        public async Task<IActionResult> PutMovie(Movie movie)
        {
            if (movie == null)
            {
                return BadRequest(new JsonResult("Movie entity is null")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (movie.Id != 0 && !MovieExists(movie.Id))
            {
                return BadRequest(new JsonResult($"The movie Id {movie.Id} don't exist, to create one set Id to 0")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (!SessionExists(movie.SessionId))
            {
                return BadRequest(new JsonResult($"The session Id {movie.Id} don't exist")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (movie.Id == 0)
            {
                await AddMovie(movie);

                return Created(new Uri($"api/Movies/{movie.Id}", UriKind.Relative), movie);
            }
            else
            {
                _dbContext.Entry(movie).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok();
            }
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_dbContext.Movies == null)
            {
                return Ok(new JsonResult($"The movie Id {id} don't exist or already deleted")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status204NoContent
                });
            }

            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return Ok(new JsonResult($"The movie Id {id} don't exist or already deleted")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status204NoContent
                });
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(long id)
        {
            return (_dbContext.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool SessionExists(long id)
        {
            return (_dbContext.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
