using EmployeeManagementSystem.Presentation.Middlewares.Exceptions;
using Serilog;

namespace EmployeeManagementSystem.Presentation.Extensions.Middlewares
{
    public static class PresentationMiddleware
    {
        public static IApplicationBuilder UsePresentationMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 1. Exception Handling Middleware (Global Exception Handling)
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            // 3. Logging Middleware (Serilog Request Logging)
            app.UseSerilogRequestLogging();

            // 4. Static Files Middleware (serve static files)
            app.UseStaticFiles();

            // 5. CORS (Cross-Origin Resource Sharing)
            app.UseCors("_AllowAll");

            // 6. Authentication Middleware (validate user identity)
            app.UseAuthentication();

            // 7. Authorization Middleware (validate user permissions)
            app.UseAuthorization();

            // 8. Status Code Pages (handle status codes)
            app.UseStatusCodePagesWithReExecute("/api/v1/errors/{0}");


            return app;
        }
    }
}