using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WebApiTesting.Api.Models;
using Xunit;

namespace WebApiTesting.Api.CodePathTests.Controllers
{
    public class PersonControllerTests : TestSetup
    {
        [Fact]
        public async Task CanGet()
        {
            // Arrange
            const int personId = 1234;
            var testPerson = new Domain.Models.Person {PersonId = personId, FirstName = "Nick", Surname = "Gowdy"};
            ExternalApiService.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(testPerson);
         
            // Act
            var response = await HttpClient.GetAsync($"person/{personId}");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseString);
            Assert.NotNull(person);
            Assert.Equal(personId, person.PersonId);
            Assert.Equal("Nick", person.FirstName);
            Assert.Equal("Gowdy", person.Surname);
        }
    }
}