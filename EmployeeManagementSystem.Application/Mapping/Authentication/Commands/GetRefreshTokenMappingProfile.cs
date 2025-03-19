using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.DTOs;

namespace EmployeeManagementSystem.Application.Mapping.Authentication.Commands
{
    public class GetRefreshTokenMappingProfile : Profile
    {
        #region Constructors

        public GetRefreshTokenMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            CreateMap<RefreshTokenDtoResponse, GetRefreshTokenCommandDto>()
                .ForMember(dest => dest.RefreshToken,
                           opt => opt.MapFrom(src => src.Token))

                .ForMember(dest => dest.RefreshTokenExpiresAt,
                           opt => opt.MapFrom(src => src.ExpiresAt));
        }

        #endregion Methods
    }
}