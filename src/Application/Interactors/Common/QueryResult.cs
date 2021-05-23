using System.Collections.Generic;

namespace AluguelIdeal.Application.Interactors.Common
{
    public class QueryResult<TDto>
    {
        public IEnumerable<TDto> Items { get; set; }
    }
}
