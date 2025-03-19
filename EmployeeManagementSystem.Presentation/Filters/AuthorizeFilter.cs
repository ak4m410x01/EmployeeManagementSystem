using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace EmployeeManagementSystem.Presentation.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        #region Fields

        private readonly string[] _userTypes;

        #endregion Fields

        #region Constructors

        public AuthorizeFilter(params string[] userTypes)
        {
            _userTypes = userTypes ?? Array.Empty<string>();
        }

        #endregion Constructors

        #region Public Methods

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Check if any user types are passed
            if (_userTypes.Length == 0)
            {
                context.Result = ErrorResponse(HttpStatusCode.Unauthorized);
                return;
            }

            // The rest of your authorization logic remains the same Get the Authorization header
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = ErrorResponse(HttpStatusCode.Unauthorized);
                return;
            }

            var token = authorizationHeader["Bearer ".Length..].Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                context.Result = ErrorResponse(HttpStatusCode.Unauthorized);
                return;
            }

            JwtSecurityToken jwtToken;
            try
            {
                jwtToken = tokenHandler.ReadJwtToken(token);
            }
            catch
            {
                context.Result = ErrorResponse(HttpStatusCode.Unauthorized);
                return;
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRoleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                context.Result = ErrorResponse(HttpStatusCode.Unauthorized);
                return;
            }

            if (string.IsNullOrWhiteSpace(userRoleClaim) || !_userTypes.Contains(userRoleClaim, StringComparer.OrdinalIgnoreCase))
            {
                context.Result = ErrorResponse(HttpStatusCode.Forbidden);
                return;
            }

            try
            {
                // Retrieve services from the DI container
                var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
                var specification = context.HttpContext.RequestServices.GetRequiredService<ISpecification<User>>();

                // Fetch the user from the database
                specification.ClearCriteriaExpressions(); // Clear existing criteria
                specification.AddCriteriaExpression(user => user.Id == userId);

                var user = await unitOfWork.Repository<User>()
                                           .FindAsNoTrackingAsync(specification);

                if (user == null)
                {
                    context.Result = ErrorResponse(HttpStatusCode.Unauthorized);
                    return;
                }

                // Check user type and token
                if (!string.Equals(user.UserType?.Name, userRoleClaim, StringComparison.OrdinalIgnoreCase))
                {
                    context.Result = ErrorResponse(HttpStatusCode.Forbidden);
                    return;
                }
            }
            catch
            {
                context.Result = ErrorResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private IActionResult ErrorResponse(HttpStatusCode statusCode, string? message = null)
        {
            var responseMessage = message ?? "Access denied. Your request cannot be processed due to invalid or insufficient credentials.";

            return new JsonResult(new Response<string>
            {
                Message = responseMessage,
                StatusCode = statusCode,
                Succeeded = false
            })
            {
                StatusCode = (int)statusCode
            };
        }

        #endregion Private Methods
    }
}