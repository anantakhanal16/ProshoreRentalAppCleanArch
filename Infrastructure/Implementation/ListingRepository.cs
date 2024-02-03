using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateListingAsync(PropertyListing listingDTO)
        {
            _context.Property.Add(listingDTO);
            await _context.SaveChangesAsync();
            return listingDTO.Id; 
        }

        public async Task<IEnumerable<PropertyListing>> GetAllListingsAsync()
        {
            return await _context.Property.ToListAsync();
        }

        public async Task<PropertyListing> GetListingByIdAsync(int id)
        {
            return await _context.Property.FindAsync(id);
        }

        public async Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDto)
        {
            var existingListing = await _context.Property.FindAsync(id);

            if (existingListing == null)
            {
                return null;
            }

            existingListing.PropertyType = listingDto.PropertyType;
            existingListing.Location = listingDto.Location;
            existingListing.Price = listingDto.Price;
            existingListing.Features = listingDto.Features;

            await _context.SaveChangesAsync();

            return existingListing;
        }

        public async Task<PropertyListing> DeleteListingAsync(int id)
        {
            var existingListing = await _context.Property.FindAsync(id);

            if (existingListing == null)
            {
                return null;
            }

            _context.Property.Remove(existingListing);
            await _context.SaveChangesAsync();

            return existingListing;
        }
    }
}
