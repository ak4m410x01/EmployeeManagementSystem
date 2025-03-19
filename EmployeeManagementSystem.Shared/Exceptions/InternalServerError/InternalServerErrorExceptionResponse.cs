using EmployeeManagementSystem.Shared.Responses;
using System.Net;

namespace EmployeeManagementSystem.Shared.Exceptions.InternalServerError
{
    public class InternalServerErrorExceptionResponse : Response<InternalServerErrorExceptionResponse>
    {
        #region Properties

        public string Details { get; set; } = string.Empty;

        #endregion Properties

        #region Constructors

        public InternalServerErrorExceptionResponse()
        {
            StatusCode = HttpStatusCode.InternalServerError;
            Message = "Internal Server Error";
            Succeeded = false;
        }

        #endregion Constructors
    }
}