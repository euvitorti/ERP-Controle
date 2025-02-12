using DTOs.Person;
using Models.People;

namespace Services.Persons
{
    public interface IPersonService
    {
        Task<Person> CreatePersonAsync(PersonDto dto);

        Task<Person?> GetPersonByIdAsync(int id);
    }
}