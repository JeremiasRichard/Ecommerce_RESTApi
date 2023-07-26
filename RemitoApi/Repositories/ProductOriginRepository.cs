using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class ProductOriginRepository : IProductOrigin
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductOriginRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(ProductOrigin productOrigin)
        {
            _dbContext.Add(productOrigin);
            return Save();
        }

        public bool Exist(int id)
        {
            return _dbContext.ProductOrigins.Any(p => p.Id == id);
        }

        public ICollection<ProductOrigin> GetAll()
        {
            return _dbContext.ProductOrigins.ToList();
        }

        public ProductOrigin GetById(int id)
        {
            return _dbContext.ProductOrigins.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Remove(ProductOrigin productOrigin)
        {
            _dbContext.Remove(productOrigin);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }

        public bool Update(ProductOrigin productOrigin)
        {
            _dbContext.Update(productOrigin);
            return Save();
        }
    }
}
