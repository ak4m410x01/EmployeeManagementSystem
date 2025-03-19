using EmployeeManagementSystem.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Application.Extensions.IQueryable
{
    public static class IQueryableExtensions
    {
        #region Paginated Response

        public static async Task<PaginatedResponse<T>> ToPaginatedQueryableAsync<T>(this IQueryable<T> source, int pageNumber = 1, int pageSize = 10, CancellationToken token = default)
        {
            int totalRecords = await source.CountAsync(token);

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return PaginatedResponse<T>.Create(items, totalRecords, pageNumber, pageSize);
        }

        public static PaginatedResponse<T> ToPaginatedQueryable<T>(this IQueryable<T> source, int pageNumber = 1, int pageSize = 10)
        {
            int totalRecords = source.Count();

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return PaginatedResponse<T>.Create(items, totalRecords, pageNumber, pageSize);
        }

        #endregion Paginated Response
    }
}