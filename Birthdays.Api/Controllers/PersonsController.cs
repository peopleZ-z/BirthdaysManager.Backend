using Birthdays.Api.Models;
using Birthdays.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Birthdays.Api.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IBirthdaysDataContext _context;
        public PersonsController(IBirthdaysDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _context.Persons.Include("Gifts").AsNoTracking().ToArrayAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var existingPerson = await _context.Persons.Where(p => p.Id == id).Include("Gifts").Include("Photos").AsNoTracking().FirstOrDefaultAsync<Person>();

            if (existingPerson != null)
            {
                return existingPerson;
            }
            else
            {
                return NotFound();
            }

            
        }

        [HttpPost]
        public async Task<ActionResult<Person>> AddPerson(Person newPerson, CancellationToken cancellationToken)
        {
            // Model validation.
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            newPerson.Id = Guid.NewGuid();

            _context.Persons.Add(newPerson);
            await _context.SaveChangesAsync(cancellationToken);

            return newPerson;
        }

        [HttpPut("id")]
        public async Task<ActionResult<Person>> Update(Person modifiedPerson, CancellationToken cancellationToken)
        {
            // Model validation.
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            // Entity tracking.
            var existingPerson = await _context.Persons.Where(p => p.Id == modifiedPerson.Id).FirstOrDefaultAsync<Person>();

            if (existingPerson != null)
            {
                existingPerson.FirstName = modifiedPerson.FirstName;
                existingPerson.LastName = modifiedPerson.LastName;

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return NotFound();
            }

            return modifiedPerson;
        }
    }
}
