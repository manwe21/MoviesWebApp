using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Data.QueryExtensions.Votes;
using Core.Application.Dto;
using Core.Application.Exceptions;
using Core.Application.Exceptions.HttpExceptions;
using Core.Application.Services.Folders;
using Core.Domain.Entities;
using Core.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services.Votes
{
    public class VoteService : IVoteService
    {
        private readonly IMovieContext _db;

        public VoteService(IMovieContext db)
        {
            _db = db;
        }

        public async Task RateMovieAsync(string userId, int movieId, int value)
        {
            var movie = await _db.Movies.FindAsync(movieId);
            if (movie == null)
                throw new ResourceNotFoundException();

            var vote = await _db.Votes.FindAsync(userId, movieId);
            if (vote == null)
            {
                vote = CreateVote(userId, movieId);
                await AddVote(vote);
            }
            
            if (vote.Value.HasValue)
                movie.ChangeVote(vote.Value.Value, value);
            else movie.AddVote(value);
            vote.Value = value;

            await _db.SaveChangesAsync();
        }

        public async Task MarkMovieAsWatchedAsync(string userId, int movieId)
        {
            var movie = await _db.Movies.FindAsync(movieId);
            if (movie == null)
                throw new ResourceNotFoundException();
            
            var vote = await _db.Votes.FindAsync(userId, movieId);
            if (vote != null)
                throw new BadRequestException();
            
            var newVote = CreateVote(userId, movieId);
            await AddVote(newVote);
            
            await _db.SaveChangesAsync();
        }
            
        public async Task UnmarkMovieAsWatchedAsync(string userId, int movieId)
        {
            var vote = await _db.Votes.FindAsync(userId, movieId);
            if (vote == null)
                throw new BadRequestException();
            var movie = await _db.Movies.FindAsync(movieId);
            if (vote.Value.HasValue)
            {
                movie.DeleteVote(vote.Value.Value);
            }

            _db.Votes.Remove(vote); 
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetVotesSimilarityAsync(string userId1, string userId2, int minMutualVotes)
        {
            var votes1 = await _db.Votes
                .GetMutualVotes(userId1, userId2)
                .Select(v => v.Value.Value)
                .ToListAsync();
            
            if (votes1.Count < minMutualVotes)
                throw new NotEnoughVotesException(minMutualVotes);
            
            var votes2 = await _db.Votes
                .GetMutualVotes(userId2, userId1)
                .Select(v => v.Value.Value)
                .ToListAsync();
            
            var res = votes1.CalcCorrelationCoefficient(votes2);
            return (int)(res * 100);
        }

        public async Task<VoteDto> GetVoteAsync(string userId, int movieId)
        {
            var vote = await _db.Votes.FindAsync(userId, movieId);
            if (vote == null)
            {
                return new VoteDto
                {
                    MovieId = movieId,
                    UserId = userId,
                    Value = 0,
                    Watched = false
                };
            }
            return new VoteDto
            {
                MovieId = vote.MovieId,
                UserId = vote.UserId,
                Value = vote.Value,
                Watched = true
            };
        }

        private Vote CreateVote(string userId, int movieId, int? value = null)
        {
            return new Vote
            {
                UserId = userId,
                MovieId = movieId,
                Date = DateTime.Now,
                Value = value
            };
        }

        private async Task AddVote(Vote vote)
        {
            await _db.Votes.AddAsync(vote);
            vote.Events.Add(new VoteCreatedEvent(vote));
        }

    }
}
    