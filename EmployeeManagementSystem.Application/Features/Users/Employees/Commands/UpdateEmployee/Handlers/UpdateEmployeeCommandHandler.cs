using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Requests;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Handlers
{
    public class UpdateEmployeeCommandHandler : ResponseHandler, IRequestHandler<UpdateEmployeeCommandRequest, Response<UpdateEmployeeCommandDto>>
    {
        #region Properties

        private readonly ISpecification<User> _specification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public UpdateEmployeeCommandHandler(ISpecification<User> specification, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _specification = specification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<UpdateEmployeeCommandDto>> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var employee = await FindEmployeeByIdAsync(request.Id, cancellationToken);
            if (employee == null)
            {
                return NotFound404<UpdateEmployeeCommandDto>();
            }

            _mapper.Map(request, employee);
            await _unitOfWork.Repository<User>()
                             .UpdateAsync(employee);

            var response = _mapper.Map<UpdateEmployeeCommandDto>(employee);
            return OK200(response);
        }

        private async Task<User?> FindEmployeeByIdAsync(int id, CancellationToken token = default)
        {
            _specification.AddCriteriaExpression(u => !u.IsDeleted && u.Id == id && u.UserType != null && u.UserTypeId == (int)UserTypes.Employee);

            var Employee = await _unitOfWork.Repository<User>()
                                            .FindAsync(_specification, token);

            return Employee;
        }

        #endregion Methods
    }
}