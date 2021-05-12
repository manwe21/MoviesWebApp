namespace Application.Exceptions.HttpExceptions
{
    public interface IHttpException
    {
        public int StatusCode { get; }
    }
}
