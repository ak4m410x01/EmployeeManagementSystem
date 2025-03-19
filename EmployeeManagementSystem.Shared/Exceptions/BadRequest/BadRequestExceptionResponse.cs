using EmployeeManagementSystem.Shared.Responses;
using System.Net;

namespace EmployeeManagementSystem.Shared.Exceptions.BadRequest
{
    public class BadRequestExceptionResponse : Response<BadRequestExceptionResponse>
    {
        #region Constructors

        public BadRequestExceptionResponse()
        {
            Message = "Bad Request";
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
        }

        #endregion Constructors
    }
}