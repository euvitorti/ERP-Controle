using DTOs.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Persons;

namespace Controllers.Persons
{
    [Authorize]
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _personService.CreatePersonAsync(personDto);

            // Retorna 201 Created, e chama o método GetById para obter a pessoa criada
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}