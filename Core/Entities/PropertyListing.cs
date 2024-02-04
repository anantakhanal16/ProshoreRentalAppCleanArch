namespace Core.Entities
{
    public class PropertyListing
    {
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string Features { get; set; }

        public ApplicationUser User { get; set; }
        public string AddedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public int ContactDetailsId { get; set; }  // Foreign key to associate contact details with a property listing
        public ContactDetails ContactDetails { get; set; }

        public List<PropertyImage> Images { get; set; }
    }
}
