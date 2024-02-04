using Core.Entities;
using Core.UseCase;

namespace ApplicationLayer.Interfaces
{
    public interface IListingService
    {
        Task<IEnumerable<PropertyListing>> GetAllListingsAsync();
        Task<PropertyListing> GetListingByIdAsync(int id);
        Task<int> CreateListingAsync(PropertyListing listingDTO);
        Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDto);
        Task<PropertyListing> DeleteListingAsync(int id);
        Task<IEnumerable<PropertyListing>> SearchPropertiesAsync(PropertySearchCriteria criteria);
    }
}
