namespace EmployeeManagementSystem.Shared.Exceptions.InternalServerError
{
    public class InternalServerErrorException : Exception
    {
        #region Constructors

        public InternalServerErrorException(string? message) : base(message)
        {
        }

        #endregion Constructors
    }
}