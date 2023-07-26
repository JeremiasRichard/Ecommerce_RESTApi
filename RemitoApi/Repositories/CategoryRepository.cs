using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;
using System.Linq;

namespace RemitoApi.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(Category category)
        {
            _dbContext.Add(category);
            return Save();
        }

        public bool Exist(int id)
        {
            return _dbContext.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetAll()
        {
            return _dbContext.Categories
         .Include(c => c.CategoryType)
         .Include(c => c.Products)
         .ThenInclude(po => po.ProductType)
         .Include(cx => cx.Products)
         .ThenInclude(pcx => pcx.ProductOrigin)
         .ToList();

        }

        public Category GetById(int id)
        {
            return _dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Remove(Category category)
        {
            _dbContext.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }

        public bool Update(Category category)
        {
            _dbContext.Update(category);
            return Save();
        }
    }
}
