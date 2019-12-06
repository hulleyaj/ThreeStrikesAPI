using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThreeStrikesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ThreeStrikesAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExpoTokensController : Controller
    {

        private readonly ExpoTokenContext _context;

        public ExpoTokensController(ExpoTokenContext context)
        {
            _context = context;

            if (_context.ExpoTokens.Count() == 0)
            {
                _context.ExpoTokens.Add(new ExpoToken { Id = 1, Token = "999" });
                _context.SaveChanges();
            }
        }

        // GET: api/ExpoTokens/999
        [HttpGet("{token}")]
        public async Task<ActionResult<IEnumerable<ExpoToken>>> GetExpoTokenByToken([FromQuery(Name = "token")] string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                return await _context.ExpoTokens.Where(f => f.Token.Equals(token, System.StringComparison.OrdinalIgnoreCase)).ToListAsync();
            }

            return await _context.ExpoTokens.ToListAsync();
        }

        // POST: api/ExpoTokens
        [HttpPost]
        public async Task<ActionResult<ExpoToken>> PostExpoToken([FromBody]ExpoToken expoToken)
        {
            ExpoToken token = await _context.ExpoTokens.Where(f => f.Token == expoToken.Token).FirstOrDefaultAsync();

            if (token == null)
            {
                _context.ExpoTokens.Add(expoToken);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetExpoTokenByToken), new { token = expoToken.Token }, expoToken);
            }
            else
            {
                return Ok();
            }     
        }

        // PUT: api/ExpoTokens
        //[HttpPut]
        //public async Task<ActionResult<ExpoToken>> PutExpoToken([FromBody]ExpoToken expoToken)
        //{
        //    _context.Entry(expoToken).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_context.ExpoTokens.Any(f => f.Token == expoToken.Token))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
    }
}