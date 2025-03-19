using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.DTOs;
using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.Requests;
using EmployeeManagementSystem.Application.Interfaces.Services.Authentication;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities.Users;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.Handlers
{
    public class GetRefreshTokenCommandHandler :
        ResponseHandler,
        IRequestHandler<GetRefreshTokenCommandRequest, Response<GetRefreshTokenCommandDto>>
    {
        #region Properties

        private readonly ISpecification<RefreshToken> _refreshTokenSpecification;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public GetRefreshTokenCommandHandler(
            ISpecification<RefreshToken> refreshTokenSpecification,
            IAuthenticationService authenticationService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _refreshTokenSpecification = refreshTokenSpecification;
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<GetRefreshTokenCommandDto>> Handle(
            GetRefreshTokenCommandRequest request,
            CancellationToken cancellationToken)
        {
            var refreshToken = await GetRefreshTokenAsync(request.RefreshToken!);
            if (refreshToken == null || refreshToken.User == null || refreshToken.User.IsDeleted)
            {
                return Unauthorized401<GetRefreshTokenCommandDto>("RefreshToken is expired or invalid.");
            }

            var token = await _authenticationService.GetRefreshTokenAsync(refreshToken?.Token ?? string.Empty);
            var response = _mapper.Map<RefreshTokenDtoResponse, GetRefreshTokenCommandDto>(token);
            return OK200(response);
        }

        private async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            _refreshTokenSpecification.AddCriteriaExpression(tk => tk.Token == token && tk.IsActive);

            var refreshToken = await _unitOfWork.Repository<RefreshToken>()
                                                .FindAsNoTrackingAsync(_refreshTokenSpecification);

            return refreshToken;
        }

        #endregion Methods
    }
}