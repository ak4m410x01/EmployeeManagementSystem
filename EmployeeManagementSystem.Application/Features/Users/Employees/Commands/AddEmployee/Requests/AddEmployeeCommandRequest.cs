using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Requests
{
    public class AddEmployeeCommandRequest : IRequest<Response<AddEmployeeCommandDto>>
    {
        #region Properties

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }

        #endregion Properties
    }
}