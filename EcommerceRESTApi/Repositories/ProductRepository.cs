using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using EcommerceRESTApi.DataBase;
using EcommerceRESTApi.Entities;
using EcommerceRESTApi.Interfaces;

namespace EcommerceRESTApi.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction _currentTransaction;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public override IQueryable<Product> GetAll()
        {
            return _dbContext.Products
            .Include(c => c.ProductOrigin)
            .Include(c => c.ProductType)
            .Include(c => c.Category)
            .AsQueryable();
        }

        public override bool Exist(int id)
        {
            return _dbContext.Products.Any(p => p.Id == id);
        }

        public override Product GetById(int id)
        {
            return _dbContext.Products.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool GetByNameOriginAndType(string productName, int originId, int typeId)
        {
            return _dbContext.Products
                 .Any(x => x.ProductName == productName && x.ProductTypeId == typeId && x.ProductOriginId == originId);
        }

        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction == null)
            {
                return _currentTransaction = _dbContext.Database.BeginTransaction();
            }
            return _currentTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                _currentTransaction?.Commit();
            }
            catch
            {
                _currentTransaction?.Rollback();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
        public void Rollback()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
