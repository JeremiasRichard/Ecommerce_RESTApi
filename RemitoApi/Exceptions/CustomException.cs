using Microsoft.CSharp.RuntimeBinder;

namespace RemitoApi.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public CustomException(string message, int statusCode):base(message)
        {
            Message = message;
            StatusCode = statusCode;
        }
        public CustomException()
        {

        }
    }
}
