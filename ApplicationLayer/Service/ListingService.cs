using ApplicationLayer.Interfaces;
using Core.Entities;
using Core.Repositories;

namespace ApplicationLayer.Service
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<int> CreateListingAsync(PropertyListing listingDTO)
        {
            var result= await _listingRepository.CreateListingAsync(listingDTO);
            return result;
        }

        public Task<PropertyListing> DeleteListingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropertyListing>> GetAllListingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PropertyListing> GetListingByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDto)
        {
            throw new NotImplementedException();
        }
    }
}
