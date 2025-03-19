using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.SignIn;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.DTOs;

namespace EmployeeManagementSystem.Application.Mapping.Authentication.Queries
{
    public class SignInMappingProfile : Profile
    {
        #region Constructors

        public SignInMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            // Map Service Response to Dto
            CreateMap<SignInDtoResponse, SignInQueryDto>()
                .ForMember(dest => dest.AccessToken,
                           opt => opt.MapFrom(src =>
                                    src.AccessToken != null ?
                                        src.AccessToken.Token :
                                        null))

            .ForMember(dest => dest.AccessTokenExpiresAt,
                           opt => opt.MapFrom(src =>
                                    src.AccessToken != null ?
                                        src.AccessToken.ExpiresAt :
                                        null))

            .ForMember(dest => dest.RefreshToken,
                           opt => opt.MapFrom(src =>
                                    src.RefreshToken != null ?
                                        src.RefreshToken.Token :
                                        null))

            .ForMember(dest => dest.RefreshTokenExpiresAt,
                           opt => opt.MapFrom(src =>
                                    src.AccessToken != null ?
                                        src.AccessToken.ExpiresAt :
                                        null));
        }

        #endregion Methods
    }
}