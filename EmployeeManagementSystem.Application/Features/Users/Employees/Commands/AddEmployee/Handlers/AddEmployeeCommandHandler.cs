using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Requests;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Handlers
{
    public class AddEmployeeCommandHandler : ResponseHandler, IRequestHandler<AddEmployeeCommandRequest, Response<AddEmployeeCommandDto>>
    {
        #region Properties

        private readonly ISpecification<User> _userSpecification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public AddEmployeeCommandHandler(ISpecification<User> userSpecification, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userSpecification = userSpecification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<AddEmployeeCommandDto>> Handle(AddEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            if (await IsEmailExistsAsync(request.Email!))
            {
                return BadRequest400<AddEmployeeCommandDto>("Email already exists.");
            }

            var employee = _mapper.Map<User>(request);
            employee.UserTypeId = (int)UserTypes.Employee;

            await _unitOfWork.Repository<User>()
                             .AddAsync(employee);

            employee = await GetEmployeeAsync(employee.Id, cancellationToken);
            var response = _mapper.Map<AddEmployeeCommandDto>(employee);

            return Created201(response);
        }

        private async Task<bool> IsEmailExistsAsync(string email, CancellationToken token = default)
        {
            _userSpecification.ClearCriteriaExpressions();
            _userSpecification.AddCriteriaExpression(u => !u.IsDeleted && u.Email == email && u.UserTypeId == (int)UserTypes.Employee);

            return await _unitOfWork.Repository<User>()
                                    .IsExistsAsync(_userSpecification, token);
        }

        private async Task<User?> GetEmployeeAsync(int id, CancellationToken token = default)
        {
            _userSpecification.ClearCriteriaExpressions();
            _userSpecification.AddCriteriaExpression(u => !u.IsDeleted && u.Id == id);

            return await _unitOfWork.Repository<User>()
                                    .FindAsNoTrackingAsync(_userSpecification, token);
        }

        #endregion Methods
    }
}