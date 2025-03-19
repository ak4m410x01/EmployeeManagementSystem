using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.Requests
{
    public class FindEmployeeByIdQueryRequest : IRequest<Response<FindEmployeeByIdQueryDto>>
    {
        #region Properties

        public int Id { get; set; }

        #endregion Properties
    }
}