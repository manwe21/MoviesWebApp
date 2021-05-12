using System;

namespace Application.Exceptions
{
    public class NotEnoughVotesException : Exception
    {
        public NotEnoughVotesException(int count) : base($"Mutual votes are less than {count}") {}
    }
}
