namespace invoice_system.Utils.Helpers.ImageHelpers;

public interface IImageService {
    Task<string> UploadImage(IFormFile Img , string subdir);
    void DeleteImage(string filepath);
    Task<bool> ValidateImage(IFormFile Img , int InMbSize = 5);
}