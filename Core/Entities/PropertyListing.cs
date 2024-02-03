namespace Core.Entities
{
    public class PropertyListing
    {
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string Features { get; set; }
    }
}
