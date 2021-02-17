using System.Collections.Generic;

namespace Infrastructure.Api
{
    public interface IApiUrlBuilder
    {
        string CreateUrl(string requestUrl, Dictionary<string, string> parameters);

    }
}
