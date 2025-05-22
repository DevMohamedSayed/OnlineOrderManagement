namespace OnlineOrderManagement.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorMessage = ex.InnerException?.Message.Contains("IX_Products_SerialNumber") == true
                    ? "Serial number must be unique. A product with the same serial number already exists."
                    : "An unexpected error occurred. Please contact support.";

                await context.Response.WriteAsJsonAsync(new { error = errorMessage });
            }
        }
    }
}
