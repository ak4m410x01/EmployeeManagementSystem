using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.DTOs;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.Requests;
using EmployeeManagementSystem.Application.Interfaces.Services.Authentication;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.Handlers
{
    public class GetAccessTokenQueryHandler :
        ResponseHandler,
        IRequestHandler<GetAccessTokenQueryRequest, Response<GetAccessTokenQueryDto>>
    {
        #region Properties

        private readonly ISpecification<RefreshToken> _specification;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public GetAccessTokenQueryHandler(
            ISpecification<RefreshToken> specification,
            IAuthenticationService authenticationService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _specification = specification;
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetAccessTokenQueryDto>> Handle(GetAccessTokenQueryRequest request, CancellationToken cancellationToken)
        {
            var refreshToken = await GetRefreshTokenAsync(request.RefreshToken!);
            if (refreshToken == null || refreshToken.User == null || refreshToken.User.IsDeleted)
            {
                return Unauthorized401<GetAccessTokenQueryDto>("RefreshToken is expired or invalid.");
            }

            var accessToken = await _authenticationService.GetAccessTokenAsync(refreshToken?.Token ?? string.Empty);
            var response = _mapper.Map<AccessTokenDtoResponse, GetAccessTokenQueryDto>(accessToken);
            return OK200(response);
        }

        private async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            _specification.AddCriteriaExpression(tk => tk.Token == token && tk.IsActive);

            var refreshToken = await _unitOfWork.Repository<RefreshToken>()
                                                .FindAsNoTrackingAsync(_specification);

            return refreshToken;
        }

        #endregion Methods
    }
}