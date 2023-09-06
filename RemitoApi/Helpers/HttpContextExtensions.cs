using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RemitoApi.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParametersInHeader<T>(this HttpContext httpContext,
           IQueryable<T> queryable, int page, int recordsPerPage)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            double quantity = await queryable.CountAsync();
            httpContext.Response.Headers.Add("TotalRegisters", quantity.ToString());
            httpContext.Response.Headers.Add("RecordsPerPage", recordsPerPage.ToString());
            httpContext.Response.Headers.Add("Page", page.ToString());

        }
    }
}
