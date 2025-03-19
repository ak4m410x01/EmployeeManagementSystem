using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.Requests
{
    public class GetAccessTokenQueryRequest : IRequest<Response<GetAccessTokenQueryDto>>
    {
        #region Properties

        public string? RefreshToken { get; set; }

        #endregion Properties
    }
}