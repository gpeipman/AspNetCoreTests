using System;
using AspNetCoreTests.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTests.UnitTests
{
    public abstract class TestBase
    {
        protected DemoDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                  .Options;
            return new DemoDbContext(options);
        }
    }
}