namespace Validator.Domain.Commands
{
    public class PaginationBaseCommand
    {
        public int Page { get; set; }
        public int Take { get; set; } = 10;
        public int Skip { get { return Take * Page; } }
    }
}
