using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.Requests;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.Handlers
{
    public class FindEmployeeByIdQueryHandler : ResponseHandler, IRequestHandler<FindEmployeeByIdQueryRequest, Response<FindEmployeeByIdQueryDto>>
    {
        #region Properties

        private readonly ISpecification<User> _userSpecification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public FindEmployeeByIdQueryHandler(ISpecification<User> userSpecification, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userSpecification = userSpecification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Public Methods

        public async Task<Response<FindEmployeeByIdQueryDto>> Handle(FindEmployeeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var employee = await FindEmployeeByIdAsync(request.Id, cancellationToken);

            if (employee == null)
            {
                return NotFound404<FindEmployeeByIdQueryDto>();
            }

            var response = _mapper.Map<FindEmployeeByIdQueryDto>(employee);

            return OK200(response);
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<User?> FindEmployeeByIdAsync(int id, CancellationToken token = default)
        {
            _userSpecification.ClearCriteriaExpressions();
            _userSpecification.AddCriteriaExpression(u => !u.IsDeleted && u.Id == id && u.UserTypeId == (int)UserTypes.Employee);

            return await _unitOfWork.Repository<User>()
                                    .FindAsNoTrackingAsync(_userSpecification, token);
        }

        #endregion Private Methods
    }
}