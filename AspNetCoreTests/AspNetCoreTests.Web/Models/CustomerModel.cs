using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreTests.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}