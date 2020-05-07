using System.Threading.Tasks;
using Refit;
using WebApiTesting.Domain.Models;

namespace WebApiTesting.Domain.Interfaces.Services
{
    public interface IExternalApiService
    {
        [Get("/users/{personId}")]
        Task<Person> GetAsync(int personId);
    }
}