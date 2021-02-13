namespace Snapkart.Contract
{
    public class PagingQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public int Skip()
        {
            return (PageNumber - 1) * PageSize;
        }
    }
}