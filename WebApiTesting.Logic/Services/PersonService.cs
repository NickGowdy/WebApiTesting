using System;
using System.Threading.Tasks;
using WebApiTesting.Domain.Interfaces.Services;
using WebApiTesting.Domain.Models;

namespace WebApiTesting.Logic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IExternalApiService _externalApiService;
        public PersonService(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }
        public async Task<Person> GetPerson(int personId)
        {
            var person = await _externalApiService.GetAsync(personId);
            return person;
        }
    }
}