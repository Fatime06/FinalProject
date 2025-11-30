using System.Net;

namespace Service.Exceptions
{
    public class CustomException : Exception
    {
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(int code, string message)
        {
            StatusCode = (HttpStatusCode)code;
            Message = message;
        }
        public CustomException(string errorKey, string errorMessage)
        {
            Errors.Add(errorKey, errorMessage);
        }
        public CustomException(int code, string errorKey, string errorMessage, string message = null)
        {
            StatusCode = (HttpStatusCode)code;
            Message = message;
            Errors.Add(errorKey, errorMessage);
        }
    }
}
