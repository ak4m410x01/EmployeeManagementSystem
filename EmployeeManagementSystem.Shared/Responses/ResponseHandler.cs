using System.Net;

namespace EmployeeManagementSystem.Shared.Responses
{
    public class ResponseHandler
    {
        #region Constructors

        public ResponseHandler()
        {
        }

        #endregion Constructors

        #region Methods

        #region Response2xx

        public Response<T> OK200<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "OK" : message,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
            };
        }

        public Response<T> OK200<T>(T data, string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "OK" : message,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Data = data
            };
        }

        public Response<T> Created201<T>(T? data, string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Created" : message,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Data = data
            };
        }

        public Response<T> Created201<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Created" : message,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true
            };
        }

        public Response<T> NoContent204<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "No Content" : message,
                StatusCode = HttpStatusCode.NoContent,
                Succeeded = true
            };
        }

        #endregion Response2xx

        #region Response4xx

        public Response<T> BadRequest400<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Bad Request" : message,
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false
            };
        }

        public Response<T> Unauthorized401<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Unauthorized" : message,
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = false
            };
        }

        public Response<T> Forbidden403<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Forbidden" : message,
                StatusCode = HttpStatusCode.Forbidden,
                Succeeded = false
            };
        }

        public Response<T> NotFound404<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Not Found" : message,
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false
            };
        }

        public Response<T> MethodNotAllowed405<T>(string? message = null)
        {
            return new Response<T>
            {
                Message = message is null ? "Method Not Allowed" : message,
                StatusCode = HttpStatusCode.MethodNotAllowed,
                Succeeded = false
            };
        }

        #endregion Response4xx

        #region Reponse5xx

        public Response<T> InternalServerError500<T>(string? message = null)
        {
            return new Response<T>()
            {
                Message = message is null ? "Internal Server Error" : message,
                StatusCode = HttpStatusCode.InternalServerError,
                Succeeded = false
            };
        }

        #endregion Reponse5xx

        #endregion Methods
    }
}