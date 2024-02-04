using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? UserRole { get; set; }

        public List<PropertyListing>? PropertyListings { get; set; }
    }
}
