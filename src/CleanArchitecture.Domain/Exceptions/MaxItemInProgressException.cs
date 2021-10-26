using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Exceptions
{
    public class MaxItemInProgressException : Exception
    {
        public MaxItemInProgressException(int maxItemsAllowed)
            : base($"Only {maxItemsAllowed} items are allowed in status 'In progress'")
        { }
    }
}
