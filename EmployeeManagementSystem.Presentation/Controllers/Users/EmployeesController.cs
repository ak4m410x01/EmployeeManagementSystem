using Asp.Versioning;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Requests;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.Requests;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Requests;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.Requests;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.DTOs;
using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.Requests;
using EmployeeManagementSystem.Domain.Enumerations.Users;
using EmployeeManagementSystem.Presentation.Controllers.Base;
using EmployeeManagementSystem.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Presentation.Controllers.Users
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/employees")]
    [AuthorizeFilter(nameof(UserTypes.Admin))]
    public class EmployeesController : BaseApiController
    {
        #region Constructors

        public EmployeesController(IMediator mediator) : base(mediator)
        {
        }

        #endregion Constructors

        #region Methods

        [HttpGet()]
        [ProducesResponseType(typeof(FindAllEmployeesQueryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FindAllEmployeesQueryDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FindAllEmployeesQueryDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FindAllEmployeesAsync([FromQuery] FindAllEmployeesQueryRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(AddEmployeeCommandDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AddEmployeeCommandDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AddEmployeeCommandDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] AddEmployeeCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FindEmployeeByIdQueryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FindEmployeeByIdQueryDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FindEmployeeByIdQueryDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(FindEmployeeByIdQueryDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindEmployeeByIdAsync(int id, [FromQuery] FindEmployeeByIdQueryRequest request)
        {
            request.Id = id;
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(UpdateEmployeeCommandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateEmployeeCommandDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UpdateEmployeeCommandDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UpdateEmployeeCommandDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, [FromBody] UpdateEmployeeCommandRequest request)
        {
            request.Id = id;
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DeleteEmployeeCommandDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DeleteEmployeeCommandDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DeleteEmployeeCommandDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DeleteEmployeeCommandDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] DeleteEmployeeCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return ResponseResult(response);
        }

        #endregion Methods
    }
}