# Web Api Testing

This project is a demonstration on how to do Web Api testing 
using Test Server. Instead of doing traditional unit tests 
where you write tests for each layer of your application.
You can use Test Server and achieve a high level of code
coverage but at the same time writing less code with lower
maintenance.

```
var server = new TestServer(new WebHostBuilder()
    .UseEnvironment("Development")
    .UseStartup<Startup>()
    .ConfigureTestServices((services) =>
    {
        //Setup injection
        SetupBoundaryModules(services);
    }));
 
HttpClient = server.CreateClient();
```
TestServer has a constructor which takes WebHostBuilder as
one of it's arguments. WebHostBuilder is where we configure everything
required so your test can reach your controller endpoints and be able
to resolve any dependency. 

```
 .UseStartup<Startup>()
```
We're going to use the configuration of your Web Api so we don't have to
worry about any IoC or any other configuration.

```
/// <summary>
/// This is where we setup our mocks
/// Typically they will be databases or other external services
/// where the process goes out of memory.
/// </summary>
/// <param name="serviceCollection"></param>
private void SetupBoundaryModules(IServiceCollection serviceCollection)
{
    serviceCollection.AddTransient(o => ExternalApiService.Object);
}
```
Here is where we setup any mock that we will need in our tests. The
philosophy of this type of testing is that we mock out any dependency
that will go out of process. In other words any external database or
service that isn't part of our codebase. We refer to this as Boundary Modules
as anything not in memory of our application is where our boundary is.

```
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
```

In this test we use *HttpClient* as defined in the setup and as we're using Test Server we only need to call
the endpoint (We don't have to worry about the base address).