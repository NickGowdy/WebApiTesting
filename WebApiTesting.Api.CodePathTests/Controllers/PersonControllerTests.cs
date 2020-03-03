using System.Threading.Tasks;
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
            using var httpClient= await GetHttpClient();
            
            // Act
            var response = await httpClient.GetAsync("person/1");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseString);
            Assert.NotNull(person);
            Assert.Equal(1, person.PersonId);
            Assert.Equal("Nick", person.FirstName);
            Assert.Equal("Gowdy", person.Surname);
        }
    }
}