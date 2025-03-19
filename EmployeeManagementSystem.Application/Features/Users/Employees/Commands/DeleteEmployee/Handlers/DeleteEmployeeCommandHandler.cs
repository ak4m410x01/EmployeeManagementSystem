using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.Requests;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.Handlers
{
    public class DeleteEmployeeCommandHandler : ResponseHandler, IRequestHandler<DeleteEmployeeCommandRequest, Response<DeleteEmployeeCommandDto>>
    {
        #region Properties

        private readonly ISpecification<User> _specification;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public DeleteEmployeeCommandHandler(ISpecification<User> specification, IUnitOfWork unitOfWork)
        {
            _specification = specification;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<DeleteEmployeeCommandDto>> Handle(DeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var employee = await FindEmployeeByIdAsync(request.Id, cancellationToken);

            if (employee == null)
            {
                return NotFound404<DeleteEmployeeCommandDto>();
            }

            employee.Email = $"Deleted-{employee.Email}-{Guid.NewGuid()}";
            employee.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<User>()
                             .UpdateAsync(employee);

            return NoContent204<DeleteEmployeeCommandDto>();
        }

        private async Task<User?> FindEmployeeByIdAsync(int id, CancellationToken token = default)
        {
            _specification.AddCriteriaExpression(u => !u.IsDeleted && u.Id == id && u.UserType != null && u.UserType.Name == nameof(UserTypes.Employee));

            var employee = await _unitOfWork.Repository<User>()
                                            .FindAsync(_specification, token);

            return employee;
        }

        #endregion Methods
    }
}