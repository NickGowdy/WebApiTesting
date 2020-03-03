using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiTesting.Domain.Interfaces.Services;
using WebApiTesting.Logic.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Person = WebApiTesting.Api.Models.Person;

namespace WebApiTesting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            
            ContainerConfiguration(services);
            
            var mapper = AutoMapperConfigurations();
            services.AddSingleton(mapper);
        }

        private static void ContainerConfiguration(IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
        }

        private static IMapper AutoMapperConfigurations()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Domain.Models.Person, Person>();
            });

            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}