using AutoMapper;
using CallForPapers.Web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CallForPapers.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            Mapper.Initialize(config => { config.CreateMissingTypeMaps = true; });

            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SubmissionsContext>();
                context.Database.EnsureCreated();
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}