using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(Product product)
        {
            _dbContext.Add(product);
            return Save();
        }

        public bool Exist(int id)
        {
            return _dbContext.Products.Any(p => p.Id == id);
        }

        public ICollection<Product> GetAll()
        {
            return _dbContext.Products
                .Include(c => c.ProductOrigin)
                .Include(c => c.ProductType)
                .Include(c => c.Category)
                .Include(c => c.Category.CategoryType)
                .ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Remove(Product product)
        {
            _dbContext.Remove(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }

        public bool Update(Product product)
        {
            _dbContext.Update(product);
            return Save();
        }
    }
}
