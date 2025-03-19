using EmployeeManagementSystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Presentation.Controllers.Errors
{
    [Route("api/v{version:apiVersion}/[controller]/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            var responseHandler = new ResponseHandler();

            return code switch
            {
                StatusCodes.Status401Unauthorized => Unauthorized(responseHandler.Unauthorized401<object>("Unauthorized or Invalid token.")),
                StatusCodes.Status403Forbidden => StatusCode(StatusCodes.Status403Forbidden, responseHandler.Forbidden403<object>("Access Forbidden.")),
                StatusCodes.Status404NotFound => NotFound(responseHandler.NotFound404<object>("Resource Not Found.")),
                StatusCodes.Status405MethodNotAllowed => StatusCode(code, responseHandler.MethodNotAllowed405<object>()),
                _ => StatusCode(code, responseHandler.InternalServerError500<object>("An unexpected error occurred."))
            };
        }
    }
}