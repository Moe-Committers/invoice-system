using invoice_system.Utils.Exceptions;

namespace invoice_system.Utils.Helpers.ImageHelpers;

public class ImageService : IImageService {
    private readonly IWebHostEnvironment _env;
    private readonly string[] _isAllowed = {".png" , ".gif" , ".jpeg" , ".jpg"};

    public ImageService(IWebHostEnvironment env){
        _env = env;
    }

    public async Task<string> UploadImage(IFormFile Img , string subdir){
        await ValidateImage(Img);
        var uploadDirectory = Path.Combine(_env.WebRootPath , "uploads" ,subdir);
        Directory.CreateDirectory(uploadDirectory);

        var ext = Path.GetExtension(Img.FileName).ToLowerInvariant();
        var filename = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(_env.WebRootPath , filename);
        using(var fileStream = new FileStream(filePath,FileMode.Create)){
            await Img.CopyToAsync(fileStream);
        }
        return $"/uploads/{subdir}/{filename}";
    }
    public void DeleteImage(string filepath){
        if(string.IsNullOrEmpty(filepath)) return;
        try{
            var path = Path.Combine(_env.WebRootPath , filepath.TrimStart('/'));
            if(File.Exists(path)){
                File.Delete(path);
            }
        }catch(Exception e){
            throw new BadRequestExceptions(e.Message);
        }
    }
    public async Task<bool> ValidateImage(IFormFile Img , int InMbSize = 5){
        if(Img.Length == 0 || Img == null){
            throw new BadRequestExceptions("invalid image");
        }
        var ext = Path.GetExtension(Img.FileName).ToLowerInvariant();
        if(!_isAllowed.Contains(ext)){
            throw new BadRequestExceptions("invalid image formats");
        }
        if(Img.Length > InMbSize * 1024 * 1024){
            throw new BadRequestExceptions("the image size is too big");
        }
        return true;
    }
}