using Data;
using DTOs.Person;
using Microsoft.EntityFrameworkCore;
using Models.People;

namespace Services.Persons
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person> CreatePersonAsync(PersonDto personDto)
        {
            // Mapeia os dados do DTO para a entidade Person
            var person = new Person
            {
                Name = personDto.Name,
                Age = personDto.Age
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}