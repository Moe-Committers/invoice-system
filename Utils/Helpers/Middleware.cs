using invoice_system.Middlewares;

namespace invoice_system.Utils.Helpers;

public static class Middleware{
    public static void UseCustomMiddleware(this IApplicationBuilder app){
        app.UseMiddleware<Example>();
    }
}