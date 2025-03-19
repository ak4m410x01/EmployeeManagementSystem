using Scalar.AspNetCore;

namespace EmployeeManagementSystem.Presentation.Extensions.Maps
{
    public static class PresentationMap
    {
        public static WebApplication AddPresentationMaps(this WebApplication app)
        {
            app.MapControllers();

            app.MapOpenApi();

            app.MapScalarApiReference();

            return app;
        }
    }
}