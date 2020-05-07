using System.Threading.Tasks;
using WebApiTesting.Domain.Models;

namespace WebApiTesting.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        public Task<Person> GetPerson(int personId);
    }
}