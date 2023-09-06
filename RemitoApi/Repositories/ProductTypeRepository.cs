using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType, int>, IProductTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override bool Exist(int id)
        {
            return _context.ProductTypes.Any(p => p.Id == id);
        }

        public override IQueryable<ProductType> GetAll()
        {
            return _context.ProductTypes
                .Include(c => c.Products)
                .ThenInclude(c => c.ProductOrigin)
                .Include(c => c.Products)
                .ThenInclude(c => c.Category)
                .AsQueryable();
        }

        public override ProductType GetById(int id)
        {
            return _context.ProductTypes.Where(x => x.Id == id)
                    .FirstOrDefault();
        }
    }
}
