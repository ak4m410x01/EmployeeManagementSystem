namespace EmployeeManagementSystem.Shared.Responses
{
    public class PaginatedRequest
    {
        #region Properties

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        #endregion Properties
    }
}