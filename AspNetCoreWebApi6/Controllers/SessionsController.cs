using AspNetCoreWebApi6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace AspNetCoreWebApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly MovieContext _dbContext;

        public SessionController(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            if (_dbContext.Sessions == null)
            {
                return NotFound();
            }
            return await _dbContext.Sessions
                         .Include("Movies")
                         .ToListAsync();
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            if (_dbContext.Sessions == null)
            {
                return NotFound();
            }
            var Session = await _dbContext.Sessions.FindAsync(id);

            if (Session == null)
            {
                return NotFound();
            }

            return Session;
        }

        [HttpGet("Day/{day}")]
        public async Task<ActionResult<Session>> GetSessionByDay(string day)
        {
            if (_dbContext.Sessions == null)
            {
                return NotFound();
            }
            var Session = await _dbContext.Sessions.Include("Movies").Where(m => m.Day.Contains(day)).ToArrayAsync();

            if (Session == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetSessionByDay", Session);
        }

        [HttpGet("Room/{room}")]
        public async Task<ActionResult<Session>> GetSessionByRoom(string room)
        {
            if (_dbContext.Sessions == null)
            {
                return NotFound();
            }
            var Session = await _dbContext.Sessions.Include("Movies").Where(m => m.Room.Contains(room)).ToArrayAsync();

            if (Session == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetSessionByRoom", Session);
        }

        private async Task AddSession(Session Session)
        {
            Session.Id = 0;
            _dbContext.Sessions.Add(Session);
            await _dbContext.SaveChangesAsync();
        }

        // PUT: api/Sessions/5
        [HttpPut()]
        public async Task<IActionResult> PutSession(Session Session)
        {
            if (Session == null)
            {
                return BadRequest(new JsonResult("Session entity is null")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (Session.Id != 0 && !SessionExists(Session.Id))
            {
                return BadRequest(new JsonResult($"The Session Id {Session.Id} don't exist, to create one set Id to 0")
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (Session.Id == 0)
            {
                await AddSession(Session);

                return Created(new Uri($"api/Sessions/{Session.Id}", UriKind.Relative), Session);
            }
            else
            {
                _dbContext.Entry(Session).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(Session.Id))
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

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            if (_dbContext.Sessions == null)
            {
                return Ok();
            }

            var Session = await _dbContext.Sessions.FindAsync(id);
            if (Session == null)
            {
                return Ok();
            }

            _dbContext.Sessions.Remove(Session);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionExists(long id)
        {
            return (_dbContext.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
