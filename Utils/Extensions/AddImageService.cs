using invoice_system.Utils.Helpers.ImageHelpers;

namespace invoice_system.Utils.Extensions;

public static class AddImageService {
    public static IServiceCollection UseImageService(this IServiceCollection service){
        service.AddScoped<IImageService , ImageService>();
        return service;
    }
}