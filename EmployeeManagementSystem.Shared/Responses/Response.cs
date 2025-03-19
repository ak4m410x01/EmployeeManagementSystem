using System.Net;

namespace EmployeeManagementSystem.Shared.Responses
{
    public class Response<T>
    {
        #region Properties

        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public bool? Succeeded { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; } = new List<string>();

        #endregion Properties

        #region Constructors

        public Response()
        {
        }

        #endregion Constructors

        #region Methods

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }

        #endregion Methods
    }
}