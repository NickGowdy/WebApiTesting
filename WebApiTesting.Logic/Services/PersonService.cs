using System;
using WebApiTesting.Domain.Interfaces.Services;
using WebApiTesting.Domain.Models;

namespace WebApiTesting.Logic.Services
{
    public class PersonService : IPersonService
    {
        public Person GetPerson(int personId)
        {
            return new Person
            {
                PersonId = 1,
                FirstName = "Nick",
                Surname = "Gowdy",
                DateOfBirth = DateTime.Now.AddYears(-32)
            };
        }
    }
}