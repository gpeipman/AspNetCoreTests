using System;
using AspNetCoreTests.Web;
using AspNetCoreTests.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTests.IntegrationTests
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DemoDbContext>();
                if(dbContext == null)
                {
                    throw new NullReferenceException("Cannot get instance of dbContext");
                }

                if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("database.windows.net"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }

                // Initialize database
            }
        }
    }
}