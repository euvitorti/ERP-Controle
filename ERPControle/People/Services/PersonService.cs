using Data;
using Microsoft.EntityFrameworkCore;
using People.DTO;
using People.Model;

namespace People.Services
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Salvar a pessoa no banco de dados
        public async Task<Person> CreatePersonAsync(PersonDto personDto)
        {
            var person = new Person
            {
                Name = personDto.Nome,
                Age = personDto.Idade 
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }

        // Buscar a pessoa pelo id
        public async Task<Person?> GetPersonByIdAsync(int id)
        {

            // Relacionando com a tabela de transações
            return await _context.Persons
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            // Retorna com os dados das transações de cada pessoa
            return await _context.Persons
                .Include(p => p.Transactions)
                .ToListAsync();
        }

        // Atualizar os dados de uma pessoa
        public async Task<Person?> UpdatePersonAsync(int id, PersonDto personDto)
        {
            // Busca a pessoa pelo id
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            
            // Retorna null se a pessoa não estiver salva no banco de dados
            if (person == null)
            {
                return null;
            }

            person.Name = personDto.Nome;
            person.Age = personDto.Idade;

            await _context.SaveChangesAsync();

            return person;
        }

        // Deletar registro da pessoa salva  
        public async Task<Person?> DeletePersonByIdAsync(int id)
        {
            // Buscar pelo id
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            
            // Se a pessoa não for encontrada, retorna null
            if (person == null)
            {
                return null;
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }
    }
}
