namespace RentalApi.DTOs
{
    public class ImageDto
    {
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
