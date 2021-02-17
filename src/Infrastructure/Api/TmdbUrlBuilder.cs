using System.Collections.Generic;
using System.Web;
using Microsoft.Extensions.Options;

namespace Infrastructure.Api
{
    public class TmdbUrlBuilder : IApiUrlBuilder
    {
        private readonly UrlBuilderOptions options;

        public TmdbUrlBuilder(IOptions<UrlBuilderOptions> queryOptions)
        {
            options = queryOptions.Value;
        }       
            
        public string CreateUrl(string requestUrl, Dictionary<string, string> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in parameters)
            {
                query[param.Key] = param.Value;
            }
            query["api_key"] = options.ApiKey;

            return requestUrl + "?" + query;
        }

    }
}
