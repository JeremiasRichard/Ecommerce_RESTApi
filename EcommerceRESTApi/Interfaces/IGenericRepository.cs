using Microsoft.EntityFrameworkCore;
using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.Interfaces
{
    public interface IGenericRepository<T, A>
    {
        T Create(T entity);
        T Update(T entity);
        T Remove(T entity);
        public T GetById(A id);
        public IQueryable<T> GetAll();
        public bool Save();
        public bool Exist(A id);
    }
}
