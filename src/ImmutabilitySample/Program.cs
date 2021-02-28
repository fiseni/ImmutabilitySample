using ImmutabilitySample.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ImmutabilitySample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dataInitializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();

                await dataInitializer.SeedAsync();
            }

            using (var scope = host.Services.CreateScope())
            {
                var myApp = scope.ServiceProvider.GetRequiredService<MyApp>();

                await myApp.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("AppConnection")));
                        services.AddScoped<DataInitializer>();
                        services.AddScoped<MyApp>();
                        services.AddScoped<IDateTime, DateTimeService>();
                        services.AddScoped<IDocumentNoGenerator, DocumentNoGenerator>();
                        services.AddScoped<IOrderService, OrderService>();
                    });
    }
}
