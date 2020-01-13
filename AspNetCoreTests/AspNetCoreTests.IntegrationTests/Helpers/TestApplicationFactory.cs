using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreTests.IntegrationTests
{
    public class TestApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                          .UseStartup<TEntryPoint>()
                          .UseSolutionRelativeContentRoot("AspNetCoreTests.Web")
                          .ConfigureAppConfiguration((context, conf) =>
                          {
                              var projectDir = Directory.GetCurrentDirectory();
                              var configPath = Path.Combine(projectDir, "appsettings.json");

                              conf.AddJsonFile(configPath);
                          });
        }
    }
}