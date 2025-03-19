using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Requests;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Mapping.Users.Employees.Commands
{
    public class AddEmployeeMappingProfile : Profile
    {
        #region Constructors

        public AddEmployeeMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            CreateMap<AddEmployeeCommandRequest, User>();

            CreateMap<User, AddEmployeeCommandDto>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src =>
                                    src.UserType != null ?
                                        src.UserType.Name :
                                        null));
        }

        #endregion Methods
    }
}