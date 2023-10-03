using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourOfHeroesWebApi.Data;
using TourOfHeroesWebApi.Models;

namespace TourOfHeroesWebApi.Controllers
{
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly HeroesContext _context;

        public HeroesController(HeroesContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes([FromQuery] string name)
        {
            // Define a LINQ query
            var heroquery = from h in _context.Heroes select h;

            // If the string is not empty, find items 'h' whose name contains the query string
            if (!String.IsNullOrEmpty(name))
            {
                heroquery = heroquery.Where(
                    h => h.Name.Contains(name));
            }
            // Return an asynchronous list of heroes that satisfy query
            return await heroquery.OrderBy(num => num.Id).ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            return hero;
        }

        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            if (hero == null)
            {
                return BadRequest();
            }
            // Generate Hero Id
            // Grab the current context of the DbSet HeroValue
            // Find the max id
            // increase by 1
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, Hero updatedHero)
        {
            if (id != updatedHero.Id)
            {
                return BadRequest();
            }

            // Check if the hero with the specified ID exists
            var existingHero = await _context.Heroes.FindAsync(id);
            if (existingHero == null)
            {
                return NotFound();
            }

            // Update the existing hero's properties with the values from updatedHero
            existingHero.Name = updatedHero.Name;
            existingHero.Power = updatedHero.Power;
            existingHero.AlterEgo = updatedHero.AlterEgo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflicts if needed
                // For example, return a conflict response if two requests try to update the same hero simultaneously
                return Conflict();
            }

            // Return a NoContent response to indicate a successful update
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();
            return hero;
        }

    }
}
