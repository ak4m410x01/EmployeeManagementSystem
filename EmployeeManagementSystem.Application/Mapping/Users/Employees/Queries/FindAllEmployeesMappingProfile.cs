using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.DTOs;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Mapping.Users.Employees.Queries
{
    public class FindAllEmployeesMappingProfile : Profile
    {
        #region Constructors

        public FindAllEmployeesMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            CreateMap<User, FindAllEmployeesQueryDto>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src =>
                                    src.UserType != null ?
                                        src.UserType.Name :
                                        null));
        }

        #endregion Methods
    }
}