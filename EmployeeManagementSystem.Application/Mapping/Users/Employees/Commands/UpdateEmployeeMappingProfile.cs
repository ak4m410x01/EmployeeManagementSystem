using AutoMapper;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Requests;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Mapping.Users.Employees.Commands
{
    public class UpdateEmployeeMappingProfile : Profile
    {
        #region Constructors

        public UpdateEmployeeMappingProfile()
        {
            InitializeMaps();
        }

        #endregion Constructors

        #region Methods

        private void InitializeMaps()
        {
            CreateMap<UpdateEmployeeCommandRequest, User>()
                .ForMember(dest => dest.Id,
                           opt => opt.Ignore())

                .ForMember(dest => dest.Name,
                           opt => opt.Condition(src =>
                                    !string.IsNullOrEmpty(src.Name)))

                .ForMember(dest => dest.Department,
                           opt => opt.Condition(src =>
                                    !string.IsNullOrEmpty(src.Department)))

                .ForMember(dest => dest.Position,
                           opt => opt.Condition(src =>
                                    !string.IsNullOrEmpty(src.Position)))

                .ForMember(dest => dest.Salary,
                           opt => opt.Condition(src =>
                                    src.Salary.HasValue));

            CreateMap<User, UpdateEmployeeCommandDto>()
                .ForMember(dest => dest.Type,
                           opt => opt.MapFrom(src =>
                                    src.UserType != null ?
                                        src.UserType.Name :
                                        null));
        }

        #endregion Methods
    }
}