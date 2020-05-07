using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using WebApiTesting.Domain.Interfaces.Services;

namespace WebApiTesting.Api.CodePathTests
{
    public class TestSetup
    {
        protected readonly Mock<IExternalApiService> ExternalApiService = new Mock<IExternalApiService>();
        
        protected HttpClient HttpClient;
        protected TestSetup()
        {
            Setup();
        }
        
        /// <summary>
        /// Setup our test server and load configuration
        /// from our startup class.
        /// </summary>
        protected void Setup()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .ConfigureTestServices((services) =>
                {
                    //Setup injection
                    SetupBoundaryModules(services);
                }));

            HttpClient = server.CreateClient();
        }

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
    }
}