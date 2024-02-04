// Infrastructure layer

using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class FileService : IFileService
{
    private readonly IHostingEnvironment _webHostEnvironment;

    public FileService(IHostingEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        var uniqueFileName = GetUniqueFileName(file.FileName);


        var uploads = _webHostEnvironment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        // Append the desired subdirectory for property uploads
        var propertyUploads = Path.Combine(uploads, "Uploads", "Property");
        // Use a default path if WebRootPath is null
     

        if (!Directory.Exists(propertyUploads))
        {
            Directory.CreateDirectory(propertyUploads);
        }

        var filePath = Path.Combine(uploads, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return filePath;
    }


    private string GetUniqueFileName(string fileName)
    {
        return Path.GetFileNameWithoutExtension(fileName)
               + "_" + Guid.NewGuid().ToString().Substring(0, 8)
               + Path.GetExtension(fileName);
    }
}
