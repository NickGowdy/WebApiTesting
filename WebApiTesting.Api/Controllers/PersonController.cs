using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTesting.Api.Models;
using WebApiTesting.Domain.Interfaces.Services;

namespace WebApiTesting.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }
        
        [HttpGet("{personId}")]
        public async Task<Person> Get(int personId)
        {
            var result = await _personService.GetPerson(personId);
            var person = _mapper.Map<Domain.Models.Person, Person>(result);
            return person;
        }
    }
}