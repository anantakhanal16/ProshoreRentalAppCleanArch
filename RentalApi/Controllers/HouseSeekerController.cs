using ApplicationLayer.Interfaces;
using ApplicationLayer.Service;
using Core.Entities;
using Core.Interfaces;
using Core.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalApi.DTOs;

namespace RentalApi.Controllers
{
    [Authorize(Roles = "HouseSeeker")]
    [Route("api/house-seeker")]
    public class HouseSeekerController : ControllerBase
    {
        private readonly IListingService _listingService;

        public HouseSeekerController(IListingService listingService )
        {
            _listingService = listingService;
        }

        [HttpGet("GetAllProperty")]
        public async Task<IActionResult> GetListings()
        {
            var listings = await _listingService.GetAllListingsAsync();


            var propertyListingsDto = listings.Select(p => new ListingDto
            {
                PropertyType = p.PropertyType,
                Location = p.Location,
                Price = p.Price,
                Features = p.Features,
                ContactDetails = new ContactDetailsDto
                {
                    Name = p.ContactDetails.Name,
                    Email = p.ContactDetails.Email,
                    Phone = p.ContactDetails.Phone
                },
                ImageDetails = p.Images?.FirstOrDefault() != null ? new ImageDto
                {

                    ImageName = p.Images.First().ImageName,
                    ImagePath = p.Images.First().ImagePath
                } : null
            }).ToList();

            return Ok(propertyListingsDto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PropertyListing>>> SearchProperties([FromQuery] PropertySearchCriteria criteria)
        {
            var properties = await _listingService.SearchPropertiesAsync(criteria);

            return Ok(properties);
        }
    }
}
