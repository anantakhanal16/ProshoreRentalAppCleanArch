using ApplicationLayer.Interfaces;
using Core.Entities;
using Core.Repositories;
using Core.UseCase;

namespace ApplicationLayer.Service
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IAuthService _authService;

        public ListingService(IListingRepository listingRepository,IAuthService authService)
        {
            _listingRepository = listingRepository;
            _authService = authService;
        }

        public async Task<int> CreateListingAsync(PropertyListing listingDTO)
        {
            var user = await _authService.GetUserDetails();
            
            listingDTO.AddedBy = user.Id.ToString();

            var result = await _listingRepository.CreateListingAsync(listingDTO);
            return result;
        }

        public async Task<PropertyListing> DeleteListingAsync(int id)
        {
            var deletedListing = await _listingRepository.DeleteListingAsync(id);
            return deletedListing;
        }

        public async Task<IEnumerable<PropertyListing>> GetAllListingsAsync()
        {
            var listings = await _listingRepository.GetAllListingsAsync();
            return listings;
        }

        public async Task<PropertyListing> GetListingByIdAsync(int id)
        {
            var listing = await _listingRepository.GetListingByIdAsync(id);
            return listing;
        }

        public async Task<IEnumerable<PropertyListing>> SearchPropertiesAsync(PropertySearchCriteria criteria)
        {
            var listing = await _listingRepository.SearchPropertiesAsync(criteria);
            return listing;
        }

        public async Task<PropertyListing> UpdateListingAsync(int id, PropertyListing listingDto)
        {
            var user = await _authService.GetUserDetails();

            listingDto.UpdatedBy = user.Id.ToString();

            var updatedListing = await _listingRepository.UpdateListingAsync(id, listingDto);

            return updatedListing;
        }
    }
}
