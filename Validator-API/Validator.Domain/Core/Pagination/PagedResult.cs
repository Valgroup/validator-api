namespace Validator.Domain.Core.Pagination
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T>? Records { get; set; }
    }
}
