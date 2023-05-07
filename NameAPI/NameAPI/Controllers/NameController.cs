using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NameAPI.Data;

namespace NameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly DataContext _context;
        public NameController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Name>>>GetNames()
        {
            return Ok(await _context.Names.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<Name>>> CreatePerson(Name name)
        {
            _context.Names.Add(name);
            await _context.SaveChangesAsync();

            return Ok(await _context.Names.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Name>>> UpdatePerson(Name name)
        {
            var person = await _context.Names.FindAsync(name.Id);
            if (person == null)
                return BadRequest("Person not found");

            person.FirstName = name.FirstName;
            person.LastName = name.LastName;
            person.Place = name.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Names.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Name>>> DeletePerson(int id)
        {
            var person = await _context.Names.FindAsync(id);
            if (person == null)
                return BadRequest("Person not found");

            _context.Names.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(await _context.Names.ToListAsync());
        }
    }
}
