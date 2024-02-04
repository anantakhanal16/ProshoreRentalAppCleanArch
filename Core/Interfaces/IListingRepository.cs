using Core.Entities;
using Core.UseCase;

namespace Core.Repositories
{
    public interface IListingRepository
    {
        Task<IEnumerable<PropertyListing>> GetAllListingsAsync();
        Task<PropertyListing> GetListingByIdAsync(int id);
        Task<int> CreateListingAsync(PropertyListing listingDTO);
        Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDto);
        Task<PropertyListing> DeleteListingAsync(int id);
        Task<IEnumerable<PropertyListing>> SearchPropertiesAsync(PropertySearchCriteria criteria);

    }
}
