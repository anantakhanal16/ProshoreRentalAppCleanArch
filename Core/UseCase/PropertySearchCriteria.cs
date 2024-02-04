namespace Core.UseCase
{
    public class PropertySearchCriteria
    {
        public string Location { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string PropertyType { get; set; }
    }
}
