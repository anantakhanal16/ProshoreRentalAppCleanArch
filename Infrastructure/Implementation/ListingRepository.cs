using Core.Entities;
using Core.UseCase;
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
            return await _context.Property
                .Include(p=>p.ContactDetails).
                Include(p=>p.Images)
                .
                ToListAsync();
        }

        public async Task<PropertyListing> GetListingByIdAsync(int id)
        {
            var list= await _context.Property
                .Include(p => p.ContactDetails)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            return list;
        }

        public async Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDTO)
        {
            var existingListing = await _context.Property
                .Include(p => p.ContactDetails)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingListing == null)
            {
                return null; 
            }

            // Update specific properties directly
            existingListing.PropertyType = listingDTO.PropertyType;
            existingListing.Location = listingDTO.Location;
            existingListing.Price = listingDTO.Price;
            existingListing.Features = listingDTO.Features;

            // Update or add ContactDetails if provided
            if (listingDTO.ContactDetails != null)
            {
                existingListing.ContactDetails = existingListing.ContactDetails ?? new ContactDetails();
                existingListing.ContactDetails.Name = listingDTO.ContactDetails.Name;
                existingListing.ContactDetails.Email = listingDTO.ContactDetails.Email;
                existingListing.ContactDetails.Phone = listingDTO.ContactDetails.Phone;
            }

            // Update or add Images if provided
            if (listingDTO.Images != null)
            {
                existingListing.Images = listingDTO.Images.Select(imageDto => new PropertyImage
                {
                    ImageName = imageDto.ImageName,
                    ImagePath = imageDto.ImagePath
                }).ToList();
            }

            await _context.SaveChangesAsync();

            // Return the updated entity
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

        public async Task<IEnumerable<PropertyListing>> SearchPropertiesAsync(PropertySearchCriteria criteria)
        {
            var query = _context.Property
                        .Include(p => p.ContactDetails)
                        .Include(p => p.Images)
          .Where(p =>
            (string.IsNullOrEmpty(criteria.Location) || p.Location.Contains(criteria.Location)) &&
            (criteria.MinPrice == 0 || p.Price >= criteria.MinPrice) &&
            (criteria.MaxPrice == 0 || p.Price <= criteria.MaxPrice) &&
            (string.IsNullOrEmpty(criteria.PropertyType) || p.PropertyType == criteria.PropertyType));

            return await query.ToListAsync();
        }
    }
}
