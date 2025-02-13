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

        // Nos métodos: GetPersonByIdAsync e GetAllPersonsAsync
        // Por padrão o EF Core ao fazer as consultas usa a forma Lazy Loading, não carrega aa relações automaticamente
        // Usar o Include para fazer as consultas

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.Include(p => p.Transactions).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.Include(p => p.Transactions).ToListAsync();
        }

        public async Task<Person?> UpdatePersonAsync(int id, PersonDto personDto)
        {
            // Busca a pessoa pelo id
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
            {
                return null;
            }

            person.Name = personDto.Name;
            person.Age = personDto.Age;

            // Atualizando o banco de dados
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<Person?> DeletePersonByIdAsync(int id)
        {
            // Busca a pessoa pelo id
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
            {
                return null;
            }

            // Apagando os dados em cascata como está configurado no ApplicationDbContext
            _context.Persons.Remove(person);
            
            await _context.SaveChangesAsync();

            return person;
        }
    }
}
