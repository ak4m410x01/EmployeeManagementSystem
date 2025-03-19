using EmployeeManagementSystem.Application.Extensions.ServiceCollections;
using EmployeeManagementSystem.Infrastructure.Extensions.ServiceCollections;
using EmployeeManagementSystem.Persistence.DataSeeding;
using EmployeeManagementSystem.Persistence.Extensions.ServiceCollections;
using EmployeeManagementSystem.Presentation.Extensions.Maps;
using EmployeeManagementSystem.Presentation.Extensions.Middlewares;
using EmployeeManagementSystem.Presentation.Extensions.ServiceCollections;

namespace EmployeeManagementSystem.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddPresentationLayer(builder.Configuration, builder.Host)
                            .AddInfrastructureLayer(builder.Configuration)
                            .AddPersistenceLayer(builder.Configuration)
                            .AddApplicationLayer();

            //builder.WebHost.ConfigureKestrel((context, options) =>
            //{
            //    options.Configure(context.Configuration.GetSection("Kestrel"));
            //});

            var app = builder.Build();

            app.UsePresentationMiddlewares(app.Environment);

            app.AddPresentationMaps();

            await DataSeeding.Initialize(app.Services.CreateAsyncScope().ServiceProvider);

            await app.RunAsync();
        }
    }
}