using EmployeeManagementSystem.Application.DTOs.Services.Authentication.SignIn;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Application.Interfaces.Services.Authentication;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Infrastructure.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Properties

        private readonly ISpecification<User> _userSpecification;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public AuthenticationService(ISpecification<User> userSpecification, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            ;
            _userSpecification = userSpecification;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public async Task<AccessTokenDtoResponse> GetAccessTokenAsync(string refreshToken)
        {
            var user = await GetUserAsync(refreshToken);
            user!.LastLogin = DateTime.UtcNow;

            await _unitOfWork.Repository<User>().UpdateAsync(user);

            return await _tokenService.GenerateAccessTokenAsync(user!);
        }

        public async Task<RefreshTokenDtoResponse> GetRefreshTokenAsync(string refreshToken)
        {
            var user = await GetUserAsync(refreshToken);

            return await _tokenService.GenerateRefreshTokenAsync(user!, true);
        }

        public async Task<SignInDtoResponse> SignInAsync(string email, string password)
        {
            _userSpecification.ClearCriteriaExpressions();
            _userSpecification.AddCriteriaExpression(u => !u.IsDeleted && u.Email == email && u.Password == password);

            var user = await _unitOfWork.Repository<User>()
                                        .FindAsync(_userSpecification);

            if (user == null)
            {
                return new SignInDtoResponse
                {
                    IsAuthenticated = false,
                    Message = "Invalid email or password."
                };
            }

            user.LastLogin = DateTime.UtcNow;
            await _unitOfWork.Repository<User>()
                             .UpdateAsync(user);

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

            return new SignInDtoResponse()
            {
                Message = "User authenticated successfully.",
                IsAuthenticated = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        #endregion Methods

        #region Private Methods

        private async Task<User?> GetUserAsync(string refreshToken)
        {
            _userSpecification.ClearCriteriaExpressions();
            _userSpecification.AddCriteriaExpression(user =>
                user.RefreshTokens != null &&
                user.RefreshTokens.Any(r => r.Token == refreshToken && r.IsActive));

            var user = await _unitOfWork.Repository<User>().FindAsync(_userSpecification);

            return user;
        }

        #endregion Private Methods
    }
}