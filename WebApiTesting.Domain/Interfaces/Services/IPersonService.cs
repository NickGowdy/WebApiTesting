using WebApiTesting.Domain.Models;

namespace WebApiTesting.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        public Person GetPerson(int personId);
    }
}