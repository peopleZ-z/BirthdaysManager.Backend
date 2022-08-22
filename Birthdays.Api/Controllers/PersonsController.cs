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
        // Shoud implement service layer.
        private readonly IBirthdaysDataContext _context;

        // Constructor DI.
        public PersonsController(IBirthdaysDataContext context)
        {
            _context = context;
        }

        // CRUD.

        /// <summary>
        /// Grabs all persons from DB.
        /// </summary>
        /// <returns>An array of persons.</returns>
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            // Sorting is not fully implemented.

            return await _context.Persons
                .OrderByDescending(p => p.DateOfBirth.Month)
                .ThenByDescending(p => p.DateOfBirth.Day)
                .Include("Gifts").AsNoTracking().ToArrayAsync();
        }

        /// <summary>
        /// Grabs one specific person from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A person or an Action Result.</returns>
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

        /// <summary>
        /// Puts the new person model to DB.
        /// </summary>
        /// <param name="newPerson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The same person model or an Action Result.</returns>
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

        /// <summary>
        /// Updates specific person entity in DB.
        /// </summary>
        /// <param name="modifiedPerson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The same person model or an Action Result.</returns>
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

        /// <summary>
        /// Delete specific person from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An Action Result</returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.Id == id);

            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
