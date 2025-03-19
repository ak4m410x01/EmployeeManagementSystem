namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.DTOs
{
    public class FindEmployeeByIdQueryDto
    {
        #region Properties

        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? LastLogin { get; set; }

        #endregion Properties
    }
}