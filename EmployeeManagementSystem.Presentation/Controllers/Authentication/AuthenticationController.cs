using Asp.Versioning;
using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.DTOs;
using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.Requests;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.DTOs;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.Requests;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.DTOs;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.Requests;
using EmployeeManagementSystem.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Presentation.Controllers.Authentication
{
    [Route("api/v{version:apiVersion}/authentication")]
    [ApiVersion("1.0")]
    public class AuthenticationController : BaseApiController
    {
        #region Constructors

        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }

        #endregion Constructors

        #region Methods

        [HttpPost("sign-in")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SignInQueryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignInQueryDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SignInQueryDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignInAsync(SignInQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpPost("access-token")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetAccessTokenQueryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAccessTokenQueryDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GetAccessTokenQueryDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccessTokenAsync(GetAccessTokenQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpPost("refresh-token")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GetRefreshTokenCommandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetRefreshTokenCommandDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GetRefreshTokenCommandDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRefreshTokenAsync(GetRefreshTokenCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        #endregion Methods
    }
}