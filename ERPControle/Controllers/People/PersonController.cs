using DTOs.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Persons;

namespace Controllers.People
{
    [Authorize]
    [ApiController]
    [Route("api/people")]
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
            // Método da classe service para buscar uma pessoa pelo id
            var person = await _personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Método da classe service para buscar todas as pessoas
            var persons = await _personService.GetAllPersonsAsync();
            return Ok(persons);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Método da classe service para atualizar uma pessoa buscando pelo id
            var updatedPerson = await _personService.UpdatePersonAsync(id, personDto);
            if (updatedPerson == null)
            {
                return NotFound();
            }

            return Ok(updatedPerson);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Método da classe service para deletar uma pessoa buscando pelo id
            var deletedPerson = await _personService.DeletePersonByIdAsync(id);
            if (deletedPerson == null)
            {
                return NotFound();
            }

            return Ok(deletedPerson);
        }
    }
}
