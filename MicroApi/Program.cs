using MicroApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace MicroApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build()
                .Run();
        }
    }

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CountryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder appBuilder)
        {

            appBuilder
                .UsePathBase(new PathString("/api"))
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/countries/{id:int}", async (context) =>
                    {
                        var countryService = context.Request.HttpContext.RequestServices.GetRequiredService<CountryService>();
                        var id = int.Parse((string)context.Request.RouteValues["id"]);
                        var country = await countryService.Get(id);
                        var response = country != null ? JsonSerializer.Serialize(country) : "country not found!";

                        await context.Response.WriteAsync(response);
                    });

                    endpoints.MapGet("/countries", async (context) =>
                    {
                        var countryService = context.Request.HttpContext.RequestServices.GetRequiredService<CountryService>();
                        var countryList = await countryService.Get();
                        var response = JsonSerializer.Serialize(countryList);

                        await context.Response.WriteAsync(response);
                    });
                });
        }
    }
}
