using AutoMapper;
using EmployeeManagementSystem.Application.Extensions.IQueryable;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.Requests;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.Handlers
{
    public class FindAllEmployeesQueryHandler :
        ResponseHandler,
        IRequestHandler<FindAllEmployeesQueryRequest, Response<FindAllEmployeesQueryDto>>
    {
        #region Properties

        private readonly ISpecification<User> _specification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public FindAllEmployeesQueryHandler(ISpecification<User> specification, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _specification = specification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<FindAllEmployeesQueryDto>> Handle(FindAllEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var employees = await FindAllEmployeesAsync(request, cancellationToken);

            var response = await _mapper.ProjectTo<FindAllEmployeesQueryDto>(employees)
                                        .ToPaginatedQueryableAsync(request.PageNumber, request.PageSize, cancellationToken);

            return response;
        }

        private async Task<IQueryable<User>> FindAllEmployeesAsync(FindAllEmployeesQueryRequest request, CancellationToken token = default)
        {
            ApplyEmployeesCriteriaExpressions(request);

            _specification.AddOrderByExpression(u => u.Id, false);

            var employees = await _unitOfWork.Repository<User>()
                                             .FindAllAsNoTrackingAsync(_specification, token);

            return employees;
        }

        private void ApplyEmployeesCriteriaExpressions(FindAllEmployeesQueryRequest request)
        {
            _specification.AddCriteriaExpression(u => !u.IsDeleted && u.UserType != null && u.UserTypeId == (int)UserTypes.Employee);

            if (!string.IsNullOrEmpty(request.q))
            {
                _specification.AddCriteriaExpression(u => EF.Functions.Like(u.Name!, $"{request.q}%"));
            }
        }

        #endregion Methods
    }
}