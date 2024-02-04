using ApplicationLayer.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalApi.DTOs;
using Core.Interfaces;
using Core.UseCase;

namespace RentalApi.Controllers
{
    [Authorize(Roles = "Broker")]
    [Route("api/broker")]
    public class BrokerController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly IFileService _fileUploadService;

        public BrokerController(IListingService listingService, IFileService fileUploadService)
        {
            _listingService = listingService;
            _fileUploadService = fileUploadService;
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

        [HttpGet("GetPropertybyId/{id}")]
        public async Task<IActionResult> GetListingById(int id)
        {
            var listing = await _listingService.GetListingByIdAsync(id);

            if (listing == null)
                return NotFound();

            var propertyListingsDto = new ListingDto
            {
                PropertyType = listing.PropertyType,
                Location = listing.Location,
                Price = listing.Price,
                Features = listing.Features,
                ContactDetails = new ContactDetailsDto
                {
                    Name = listing.ContactDetails.Name,
                    Email = listing.ContactDetails.Email,
                    Phone = listing.ContactDetails.Phone
                },
                ImageDetails = listing.Images?.FirstOrDefault() != null ? new ImageDto
                {
                  
                    ImageName = listing.Images.First().ImageName,
                    ImagePath = listing.Images.First().ImagePath
                } : null
            };

            return Ok(propertyListingsDto);
        }

        [HttpPost("listings/AddProperty")]
        public async Task<IActionResult> CreateListing([FromForm] AddPropertyDto listingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if there are images in the request
            if (listingDTO.ImageFiles == null || listingDTO.ImageFiles.Count == 0)
            {
                return BadRequest("At least one image is required.");
            }

            
            var uploadedImages = new List<PropertyImage>();

            foreach (var imageFile in listingDTO.ImageFiles)
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Invalid image file.");
                }

              
                var imagePath = await _fileUploadService.UploadFileAsync(imageFile);

                if (string.IsNullOrEmpty(imagePath))
                {
                    return BadRequest("Failed to upload one or more images.");
                }

                uploadedImages.Add(new PropertyImage
                {
                    ImageName = imageFile.FileName,
                    ImagePath = imagePath
                });
            }

            var model = new PropertyListing
            {
                PropertyType = listingDTO.PropertyType,
                Location = listingDTO.Location,
                Price = listingDTO.Price,
                Features = listingDTO.Features,
                ContactDetails = new ContactDetails
                {
                    Name = listingDTO.ContactDetails.Name,
                    Email = listingDTO.ContactDetails.Email,
                    Phone = listingDTO.ContactDetails.Phone
                },
                Images = uploadedImages
            };

          
            var createdListing = await _listingService.CreateListingAsync(model);

            return Ok("Sucess");
        }

        [HttpPatch("UpdateProperty/{id}")]
        public async Task<IActionResult> UpdateListing(int id, [FromForm] UpdateProperty listingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uploadedImages = new List<PropertyImage>();

            foreach (var imageFile in listingDTO.ImageFiles)
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Invalid image file.");
                }
                var imagePath = await _fileUploadService.UploadFileAsync(imageFile);

                if (string.IsNullOrEmpty(imagePath))
                {
                    return BadRequest("Failed to upload one or more images.");
                }

                uploadedImages.Add(new PropertyImage
                {
                    ImageName = imageFile.FileName,
                    ImagePath = imagePath
                });
            }

            var model = new PropertyListing
            {
                PropertyType = listingDTO.PropertyType,
                Location = listingDTO.Location,
                Price = listingDTO.Price,
                Features = listingDTO.Features,
                ContactDetails = new ContactDetails
                {
                    Name = listingDTO.ContactDetails.Name,
                    Email = listingDTO.ContactDetails.Email,
                    Phone = listingDTO.ContactDetails.Phone
                },
                Images = uploadedImages
            };

            var updatedListing = await _listingService.UpdateListingAsync(id, model);
          
            return Ok("Updated");
        }


        [HttpDelete("DeleteProperty/{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            var deletedListing = await _listingService.DeleteListingAsync(id);

            if (deletedListing == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PropertyListing>>> SearchProperties([FromQuery] PropertySearchCriteria criteria)
        {
            var properties = await _listingService.SearchPropertiesAsync(criteria);

            return Ok(properties);
        }
    }
}
