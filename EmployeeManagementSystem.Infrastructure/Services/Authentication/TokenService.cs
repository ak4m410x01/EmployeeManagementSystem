using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Application.Interfaces.Services.Authentication;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Entities.Users;
using EmployeeManagementSystem.Shared.Handlers.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementSystem.Infrastructure.Services.Authentication
{
    public class TokenService : ITokenService
    {
        #region Properties

        private const string DefaultJwtKey = "sz8eI7OdHBrjrIo8j9nTW/rQyO1OvY0pAQ2wDKQZw/0=";
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public TokenService(JwtConfiguration jwtConfiguration, IUnitOfWork unitOfWork)
        {
            _jwtConfiguration = jwtConfiguration;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public async Task<AccessTokenDtoResponse> GenerateAccessTokenAsync(User user)
        {
            JwtSecurityToken token = new
            (
               issuer: _jwtConfiguration.Issuer,
               audience: _jwtConfiguration.Audience,
               expires: DateTime.UtcNow.AddDays(_jwtConfiguration.AccessTokenExpiryDays),
               claims: GetTokenClaims(user),
               signingCredentials: GetSigningCredentials()
            );

            var tokenDto = new AccessTokenDtoResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = token.ValidTo
            };

            return await Task.FromResult(tokenDto);
        }

        private SigningCredentials GetSigningCredentials()
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfiguration.Key ?? DefaultJwtKey));
            return new(key, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetTokenClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserType!.Name!.ToString())
            };

            return claims;
        }

        public async Task<RefreshTokenDtoResponse> GenerateRefreshTokenAsync(User user, bool revokeOld = false)
        {
            var refreshToken = user.RefreshTokens?.FirstOrDefault(token => token.IsActive);

            if (refreshToken == null)
            {
                refreshToken = GenerateRefreshTokenAsync();
                refreshToken.UserId = user.Id;
                await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            }
            else if (revokeOld)
            {
                refreshToken.RevokedAt = DateTime.UtcNow;
                await _unitOfWork.SaveAsync();

                refreshToken = GenerateRefreshTokenAsync();
                refreshToken.UserId = user.Id;
                await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            }

            return new RefreshTokenDtoResponse()
            {
                Token = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresAt
            };
        }

        private RefreshToken GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Fill(randomNumber);

            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpiryDays),
                CreatedAt = DateTime.UtcNow
            };
        }

        #endregion Methods
    }
}