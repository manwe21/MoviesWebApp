using System;

namespace Application.Exceptions.HttpExceptions
{
    public class AccessDeniedException : Exception, IHttpException
    {
        public int StatusCode => 403;
        
        public AccessDeniedException() : base("Access denied"){}
        
    }
}
