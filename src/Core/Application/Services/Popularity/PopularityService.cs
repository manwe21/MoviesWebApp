using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Data.QueryExtensions.Pagination;
using Core.Application.Data.Repositories;
using Core.Application.Dto;
using Core.Domain.Entities;

namespace Core.Application.Services.Popularity
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
