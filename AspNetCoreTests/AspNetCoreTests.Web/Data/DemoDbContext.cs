using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTests.Web.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}