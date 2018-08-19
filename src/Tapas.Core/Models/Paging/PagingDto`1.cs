namespace Tapas.Core.Models.Paging
{
    using System.Collections.Generic;

    public class PagingDto< T >
    {
        public string Search { get; set; }
        public int MaxPages { get; set; }
        public int MaxRows { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}