using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.DTOs;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.Requests
{
    public class DeleteEmployeeCommandRequest : IRequest<Response<DeleteEmployeeCommandDto>>
    {
        #region Properties

        public int Id { get; set; }

        #endregion Properties
    }
}