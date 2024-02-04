namespace Core.Entities
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public int PropertyListingId { get; set; }  // Foreign key to associate the image with a property listing
        public PropertyListing PropertyListing { get; set; }
    }    
  }

