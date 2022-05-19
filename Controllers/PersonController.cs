using ApiPerson_Check.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiPersonCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        readonly PersonDbContext db;
        public PersonsController(PersonDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var persons = db.Persons.ToList();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = db.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post(Person model)
        {
            db.Persons.Add(model);
            db.SaveChanges();
            return CreatedAtAction("Get", routeValues: new
            {
                id = model.Id
            }, value: model);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, Person model)
        {
            var person = db.Persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            if (person != null)
            {
                person.Name = model.Name;
                person.Surname = model.Surname;
                person.Description = model.Description;
                db.SaveChanges();
            }
            return AcceptedAtAction("Get", new
            {
                id = person.Id
            }, person);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = db.Persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            if (person != null)
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            return NoContent();
        }
    }
}
