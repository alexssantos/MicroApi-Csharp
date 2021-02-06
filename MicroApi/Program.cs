using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

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
        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            appBuilder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("api/", async (context) =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    });
                });
        }
    }
}
