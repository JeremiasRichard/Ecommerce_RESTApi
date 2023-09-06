using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;
using System.Linq;

namespace RemitoApi.Repositories
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public override IQueryable<Category> GetAll()
        {
            return _dbContext.Categories
            .Include(c => c.Products)
            .ThenInclude(po => po.ProductType)
            .Include(cx => cx.Products)
            .ThenInclude(pcx => pcx.ProductOrigin)
            .AsQueryable();
        }

        public override Category GetById(int id)
        {
            return _dbContext.Categories.Where(x => x.Id == id)
            .Include(c => c.Products)
            .ThenInclude(po => po.ProductType)
            .Include(cx => cx.Products)
            .ThenInclude(pcx => pcx.ProductOrigin).
            FirstOrDefault();
        }

        public override bool Exist(int id)
        {
            return _dbContext.Categories.Any(x => x.Id == id);
        }

        public Category GetByName(string name)
        {
            return _dbContext.Categories.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }
    }
}
