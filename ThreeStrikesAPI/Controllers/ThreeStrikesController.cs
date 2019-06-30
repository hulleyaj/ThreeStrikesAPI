using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeStrikesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThreeStrikesAPI.Controllers
{
    [Route("api/[controller]")]
    public class ThreeStrikesController : Controller
    {

        private readonly ThreeStrikesItemContext _context;

        public ThreeStrikesController(ThreeStrikesItemContext context)
        {
            _context = context;

            if (_context.ThreeStrikesItems.Count() == 0)
            {
                // Create a new ThreeStrikesItems if collection is empty,
                // which means you can't delete all ThreeStrikesItems.
                _context.ThreeStrikesItems.Add(new ThreeStrikesItem { Item = "Empty", Price = 0 });
                _context.SaveChanges();
            }
        }

        // GET: api/ThreeStrikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThreeStrikesItem>>> GetThreeStrikesItems()
        {
            return await _context.ThreeStrikesItems.ToListAsync();
        }

        // GET: api/ThreeStrikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThreeStrikesItem>> GetThreeStrikesItemById(int id)
        {
            ThreeStrikesItem threeStrikesItem = await _context.ThreeStrikesItems.FindAsync(id);

            if (threeStrikesItem == null)
            {
                return NotFound();
            }

            return threeStrikesItem;
        }

        // Get: api/ThreeStrikes?item=ferrari
        //[HttpGet()]
        //public async Task<ActionResult<ThreeStrikesItem>> GetThreeStrikesItemByName([FromQuery(Name = "item")] string item)
        //{
        //    ThreeStrikesItem threeStrikesItem = await _context.ThreeStrikesItems.

        //    return threeStrikesItem;
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
