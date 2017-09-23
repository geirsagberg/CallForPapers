using AutoMapper;
using CallForPapers.Web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace CallForPapers.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            var webHost = BuildWebHost(args);

            Mapper.Initialize(config => config.CreateMissingTypeMaps = true);

            using (var scope = webHost.Services.CreateScope()) {
                var context = scope.ServiceProvider.GetRequiredService<SubmissionContext>();
                context.Database.EnsureCreated();
            }

            webHost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}