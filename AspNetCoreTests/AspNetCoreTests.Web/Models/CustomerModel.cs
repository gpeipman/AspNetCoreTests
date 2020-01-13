using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreTests.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}