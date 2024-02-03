using System.ComponentModel.DataAnnotations;

namespace RentalApi.DTOs
{
    public class ListingDto
    {
        [Required(ErrorMessage = "Property Type is required")]
        public string PropertyType { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "Features cannot exceed 1000 characters")]
        public string Features { get; set; }
    }
}
