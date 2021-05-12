using System.Threading.Tasks;
using Application.Data.QueryExtensions.Pagination;
using Application.Data.Repositories;
using Application.Dto;
using AutoMapper;

namespace Application.Services.Popularity
{
    public class PopularityService : IPopularityService
    {
        private readonly IPopularityRepository _popularityRepository;
        private readonly IMapper _mapper;

        public PopularityService(IPopularityRepository popularityRepository, IMapper mapper)
        {
            _popularityRepository = popularityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<MovieDto>> ListPopularMoviesAsync(int pageSize, int pageNumber)
        {
            var movies = await _popularityRepository.ListPopularMoviesAsync(pageSize, pageNumber);
            return _mapper.Map<PagedResult<MovieDto>>(movies);
        }
    }
}
