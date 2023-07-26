using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class ProductTypeRepository : IProductType
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(ProductType productType)
        {
            _dbContext.Add(productType);
            return Save();
        }

        public bool Exist(int id)
        {
          return  _dbContext.ProductTypes.Any(p => p.Id == id);
        }

        public ICollection<ProductType> GetAll()
        {
            return _dbContext.ProductTypes.ToList();
        }

        public ProductType GetById(int id)
        {
          return  _dbContext.ProductTypes.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Remove(ProductType productType)
        {
            _dbContext.Remove(productType);
            return Save();
        }

        public bool Save()
        {
           var saved = _dbContext.SaveChanges();
           return saved > 0;
        }

        public bool Update(ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
