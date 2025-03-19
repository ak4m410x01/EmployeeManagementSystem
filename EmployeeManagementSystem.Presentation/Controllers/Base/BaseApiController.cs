using EmployeeManagementSystem.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagementSystem.Presentation.Controllers.Base
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        #region Properties

        public IMediator Mediator { get; protected set; }

        #endregion Properties

        #region Constructors

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        #endregion Constructors

        #region Methods

        public IActionResult ResponseResult<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created("", response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.NoContent => Ok(response),
                _ => Ok(response)
            };
        }

        #endregion Methods
    }
}