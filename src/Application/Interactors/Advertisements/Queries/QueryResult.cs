using System.Collections.Generic;

namespace AluguelIdeal.Application.Interactors.Advertisements.Queries
{
    public class QueryResult<TDto>
    {
        public IEnumerable<TDto> Items { get; set; }
    }
}
