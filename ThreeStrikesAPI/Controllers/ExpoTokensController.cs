using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThreeStrikesAPI.Models;

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
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpoToken>> GetExpoTokenById(int id)
        {
            ExpoToken expoToken = await _context.ExpoTokens.FindAsync(id);

            if (expoToken == null)
            {
                return NotFound();
            }

            return expoToken;
        }

        // POST: api/ExpoTokens
        [HttpPost]
        public async Task<ActionResult<ExpoToken>> PostExpoToken([FromBody]ExpoToken expoToken)
        {
            _context.ExpoTokens.Add(expoToken);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpoTokenById), new { Id = expoToken.Id }, expoToken);
        }
    }
}