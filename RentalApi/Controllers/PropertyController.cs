using ApplicationLayer.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RentalApi.DTOs;

namespace RentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IListingService _listingService;

        public PropertyController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool? isActive = null)
        {
            IEnumerable<PropertyListing> listings = await _listingService.GetAllListingsAsync();
            return Ok(listings);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ListingDto listingDto)
        {
            if (ModelState.IsValid)
            {
                PropertyListing propertyListing = new PropertyListing
                {
                    PropertyType = listingDto.PropertyType,
                    Location = listingDto.Location,
                    Price = listingDto.Price,
                    Features = listingDto.Features
                };
                var createdListingId = await _listingService.CreateListingAsync(propertyListing);

                return CreatedAtAction(nameof(GetPropertyById), new { id = createdListingId }, "Added Successfully");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropertyDetails(int id, [FromBody] ListingDto listingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PropertyListing propertyListing = new PropertyListing
            {
                PropertyType = listingDto.PropertyType,
                Location = listingDto.Location,
                Price = listingDto.Price,
                Features = listingDto.Features
            };

            var updatedListing = await _listingService.UpdateListingAsync(id, propertyListing);

            if (updatedListing == null)
            {
                return NotFound();
            }

            return Ok(updatedListing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedListing = await _listingService.DeleteListingAsync(id);

            if (deletedListing == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await _listingService.GetListingByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }
    }
}
