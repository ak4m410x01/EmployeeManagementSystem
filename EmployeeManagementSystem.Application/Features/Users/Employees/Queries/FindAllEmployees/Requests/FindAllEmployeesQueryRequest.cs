using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.Requests
{
    public class FindAllEmployeesQueryRequest : PaginatedRequest, IRequest<Response<FindAllEmployeesQueryDto>>
    {
        #region Properties

        public string? q { get; set; }

        #endregion Properties
    }
}