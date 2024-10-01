namespace BasicInvoiceApp.Application.Helper
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int TotalRecords { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedList(List<T> items, int totalRecords, int pageNumber, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
