using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.DTOs;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Mapping.Users.Employees.Queries
{
    public class FindEmployeeByIdMappingProfile : Profile
    {
        #region Constructors

        public FindEmployeeByIdMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            CreateMap<User, FindEmployeeByIdQueryDto>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src =>
                                    src.UserType != null ?
                                        src.UserType.Name :
                                        null));
        }

        #endregion Methods
    }
}