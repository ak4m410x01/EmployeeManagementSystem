using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.Requests
{
    public class GetRefreshTokenCommandRequest : IRequest<Response<GetRefreshTokenCommandDto>>
    {
        #region Properties

        public string? RefreshToken { get; set; }

        #endregion Properties
    }
}