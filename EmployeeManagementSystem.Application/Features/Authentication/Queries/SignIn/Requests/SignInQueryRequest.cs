using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.Requests
{
    public class SignInQueryRequest : IRequest<Response<SignInQueryDto>>
    {
        #region Properties

        public string? Email { get; set; }
        public string? Password { get; set; }

        #endregion Properties
    }
}