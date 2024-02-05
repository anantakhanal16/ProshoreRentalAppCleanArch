using Core.Entities;
using Core.UseCase;

namespace Core.Repositories
{
    public interface IListingRepository
    {
        Task<IEnumerable<PropertyListing>> GetAllListingsAsync();
        Task<IEnumerable<PropertyListing>> GetAllListingsByUserId(string userId);
        Task<PropertyListing> GetListingByIdAsync(int id);
        Task<int> CreateListingAsync(PropertyListing listingDTO);
        Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDto,string userId);
        Task<PropertyListing> DeleteListingAsync(int id, string userid);
        Task<IEnumerable<PropertyListing>> SearchPropertiesAsync(PropertySearchCriteria criteria);

    }
}
