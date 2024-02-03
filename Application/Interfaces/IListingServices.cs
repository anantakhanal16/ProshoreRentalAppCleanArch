using ProshoreRentalApp.DTOs;

namespace Application.Interfaces
{
    public interface IListingServices
    {
        Task<IEnumerable<ListingDTO>> GetAllListingsAsync();
        Task<ListingDTO> GetListingByIdAsync(int id);
        Task<int> CreateListingAsync(ListingDTO listingDTO);
        Task UpdateListingAsync(int id, ListingDTO listingDTO);
        Task DeleteListingAsync(int id);
    }
}
