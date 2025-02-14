namespace invoice_system.Middlewares;

public class Examplee
{
    private readonly RequestDelegate _next;
    private readonly ILogger<Examplee> _logger;
    public Examplee(RequestDelegate next, ILogger<Examplee> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext http)
    {
        if (http.Request.Path.StartsWithSegments("/api/example"))
        {
            _logger.LogInformation("hellow form example middleware");
        }
        await _next(http);
    }
}