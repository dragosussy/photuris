namespace photuris_backend.Extensions.Pagination
{
    public static class PagingExtensions
    {
        public static Page<T> ToPages<T>(this IQueryable<T> allRecords, int pageNumber, int pageSize)
        {
            var items = allRecords.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new Page<T>
            {
                Contents = items,
                PageNr = pageNumber,
                PageSize = pageSize
            };
        }

        public static Page<T> ToPages<T>(this ICollection<T> allRecords, int pageNumber, int pageSize)
        {
            var items = allRecords.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new Page<T>
            {
                Contents = items,
                PageNr = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
