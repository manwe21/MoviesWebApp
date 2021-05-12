using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Application.Data.QueryExtensions.Pagination;
using Application.Data.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Api;
using Infrastructure.Api.JsonObjects;

namespace Infrastructure.Data.Repositories
{
    public class ApiPopularityRepository : IPopularityRepository
    {
        private readonly HttpClient _httpClient;
        private readonly TmdbUrlBuilder _urlBuilder;
        private readonly IMapper _mapper;

        public ApiPopularityRepository(HttpClient httpClient, TmdbUrlBuilder urlBuilder, IMapper mapper)
        {
            _httpClient = httpClient;
            _urlBuilder = urlBuilder;
            _mapper = mapper;
        }
            
        public async Task<PagedResult<Movie>> ListPopularMoviesAsync(int pageSize, int pageNumber)
        {
            var queryParams = new Dictionary<string, string>
            {
                {
                    "page", $"{pageNumber}"
                }
            };
            var url = _urlBuilder.CreateUrl("movie/popular", queryParams);
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new PagedResult<Movie>();
            }

            PagedResultJson<MovieJson> pagedMovies;
            await using (var stream = await response.Content.ReadAsStreamAsync())
            {
                pagedMovies = await JsonSerializer.DeserializeAsync<PagedResultJson<MovieJson>>(stream);
            }

            var result = _mapper.Map<PagedResult<Movie>>(pagedMovies);
            result.Results = result.Results.Take(pageSize).ToList();
            return result;
        }   
    }
}   
