using Microsoft.EntityFrameworkCore;

namespace RemitoApi.DataBase
{
    public class Pagination <T> : List<T>
    {
        public  int StartPage { get; private set; }
        public int TotalPages { get; private set; }

        public Pagination(List<T> items, int count, int startPage, int registerQuantity)
        {
            StartPage = startPage;
            TotalPages = (int)Math.Ceiling(count/(double)registerQuantity);
            
            AddRange(items);
        }
        public bool PreviousPages => StartPage < 1;
        public bool NextPages => StartPage < TotalPages;

        public static async Task<Pagination<T>> CreatePagination(IQueryable<T> font, int startPage, int registerQuentity)
        {
            var count = await font.CountAsync();
            var items = await font.Skip((startPage - 1)* registerQuentity).Take(registerQuentity).ToListAsync();
            return new Pagination<T>(items, count, startPage, registerQuentity);
        }
    }
}
