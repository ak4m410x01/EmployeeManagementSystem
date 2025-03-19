using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace EmployeeManagementSystem.Presentation.Extensions.ServiceCollections.Authentication
{
    public static class AuthenticationOptions
    {
        public static AuthenticationBuilder AddAuthenticationOptions(this IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        public static AuthenticationBuilder AddJwtBearerOptions(this AuthenticationBuilder services, IConfiguration configuration)
        {
            services.AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Authentication:JwtConfiguration:Issuer"],
                    ValidAudience = configuration["Authentication:JwtConfiguration:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtConfiguration:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        var result = JsonSerializer.Serialize(new { error = "Token is not provided or invalid." });

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        context.HandleResponse();
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            return services;
        }
    }
}