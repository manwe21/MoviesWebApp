using System;

namespace Application.Exceptions.HttpExceptions
{
    public class ResourceNotFoundException : Exception, IHttpException
    {
        public int StatusCode => 404;

        public ResourceNotFoundException() : base("Resource not found")
        {
            
        }
    }
}
