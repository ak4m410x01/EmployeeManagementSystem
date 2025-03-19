namespace EmployeeManagementSystem.Shared.Exceptions.BadRequest
{
    public class BadRequestException : Exception
    {
        #region Constructors

        public BadRequestException(string? message) : base(message)
        {
        }

        #endregion Constructors
    }
}