using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbSyncAlgorithm.Models;

namespace DbSyncAlgorithm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DbSyncAlgorithmContext _context;

        public PeopleController(DbSyncAlgorithmContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPerson()
        {
            return _context.Person;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] Guid id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // POST: api/People/batch
        [HttpPost("batch")]
        public async Task<IActionResult> PostPerson([FromBody] ICollection<Person> people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Person.AddRange(people);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { ids = people.Select(p => p.Id) }, people);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        // DELETE: api/People/
        [HttpDelete]
        public async Task<IActionResult> DeletePerson([FromBody] ICollection<Guid> guids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Person.RemoveRange(guids.Select(guid => new Person { Id = guid }));
            await _context.SaveChangesAsync();

            return Ok(guids);
        }

        private bool PersonExists(Guid id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}