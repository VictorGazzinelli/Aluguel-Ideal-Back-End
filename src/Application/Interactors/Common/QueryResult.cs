using System;
using System.Collections.Generic;
using System.Linq;

namespace AluguelIdeal.Application.Interactors.Common
{
    public class QueryResult<TDto>
    {
        public IEnumerable<TDto> Items { get; set; }

        public QueryResult()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is QueryResult<TDto> result &&
                   Items.SequenceEqual(result.Items);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Items);
        }
    }
}
