using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalApi.DTOs
{
    public class UpdateProperty
    {
        [Required(ErrorMessage = "Property type is required.")]
        public string PropertyType { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        public string Features { get; set; }

        public ContactDetailsDto ContactDetails { get; set; }

        // Assuming you're using ASP.NET Core for handling file uploads
        [Required(ErrorMessage = "Please select image files.")]
        [DataType(DataType.Upload)]
        public List<IFormFile> ImageFiles { get; set; }
    }
}
