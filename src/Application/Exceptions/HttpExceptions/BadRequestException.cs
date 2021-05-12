using System;

namespace Application.Exceptions.HttpExceptions
{
    public class BadRequestException : Exception, IHttpException
    {
        public int StatusCode => 400;

        public BadRequestException() : base("Bad request")
        {
            
        }
        
    }
}
