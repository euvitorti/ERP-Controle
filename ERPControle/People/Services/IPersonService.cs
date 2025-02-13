using People.DTO;
using People.Model;

namespace People.Services
{
    public interface IPersonService
    {
        Task<Person> CreatePersonAsync(PersonDto dto);
        Task<Person?> GetPersonByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person?> UpdatePersonAsync(int id, PersonDto dto);
        Task<Person?> DeletePersonByIdAsync(int id);
    }
}
