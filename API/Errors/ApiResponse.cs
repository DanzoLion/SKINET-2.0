using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);   // if ?? then execute method on the right of ?? 
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)   // based on our statusCode we return our own default messages
        {
           return statusCode switch
           {
               400 => "A bad request, you have made",
               401 => "Authorized, you are not",
               404 => "Resource found, it was not",
               500 => "Errors are the path to the dar side.  Errors lead to anger.  Anger leads to hate.  Hate leads to career change ",
               _ => null            // _ is the default case and if no match return null
           };
        }


    }
}