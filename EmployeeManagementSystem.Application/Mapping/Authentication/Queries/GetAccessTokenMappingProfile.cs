using AutoMapper;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.DTOs;

namespace EmployeeManagementSystem.Application.Mapping.Authentication.Queries
{
    public class GetAccessTokenMappingProfile : Profile
    {
        #region Constructors

        public GetAccessTokenMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            CreateMap<AccessTokenDtoResponse, GetAccessTokenQueryDto>()
                .ForMember(dest => dest.AccessToken,
                           opt => opt.MapFrom(src => src.Token))

                .ForMember(dest => dest.AccessTokenExpiresAt,
                           opt => opt.MapFrom(src => src.ExpiresAt));
        }

        #endregion Methods
    }
}