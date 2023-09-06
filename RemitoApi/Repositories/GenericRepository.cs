using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class GenericRepository<T, A> : IGenericRepository<T, A> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public virtual T GetById(A id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public virtual bool Exist(A id)
        {
            return _context.Set<T>().Any();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
