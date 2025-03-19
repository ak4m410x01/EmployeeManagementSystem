namespace EmployeeManagementSystem.Application.Extensions.IEnumerable
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ToPaginatedEnumerable<T>(this IEnumerable<T> source, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return items;
        }
    }
}