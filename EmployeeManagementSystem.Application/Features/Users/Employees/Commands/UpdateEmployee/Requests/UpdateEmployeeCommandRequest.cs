using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Requests
{
    public class UpdateEmployeeCommandRequest : IRequest<Response<UpdateEmployeeCommandDto>>
    {
        #region Properties

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }

        #endregion Properties
    }
}