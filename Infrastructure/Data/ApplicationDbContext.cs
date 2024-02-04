using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PropertyListing> Property { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(pi => pi.PropertyListing)
                .WithMany(pl => pl.Images)
                .HasForeignKey(pi => pi.PropertyListingId);

            modelBuilder.Entity<PropertyListing>()
               .HasOne(pl => pl.ContactDetails)
               .WithOne()
               .HasForeignKey<PropertyListing>(pl => pl.ContactDetailsId);
        }
    }
}
