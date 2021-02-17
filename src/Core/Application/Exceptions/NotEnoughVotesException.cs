using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Exceptions
{
    public class NotEnoughVotesException : Exception
    {
        public NotEnoughVotesException(int count) : base($"Mutual votes are less than {count}") {}
    }
}
