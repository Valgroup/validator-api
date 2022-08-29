namespace Validator.Domain.Core.Pagination
{
    public interface IPagedResult<T>
    {
        int RecordsTotal { get; set; }
        int RecordsFiltered { get; set; }
        IEnumerable<T>? Records { get; set; }
    }
}
