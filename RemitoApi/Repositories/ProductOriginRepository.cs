﻿using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class ProductOriginRepository : GenericRepository<ProductOrigin, int>, IProductOriginRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductOriginRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<ProductOrigin> GetAll()
        {
            return _context.ProductOrigins
                .Include(p => p.Products)
                .ThenInclude(c => c.Category)
                .AsQueryable();
        }

        public override ProductOrigin GetById(int id)
        {
            return _context.ProductOrigins.Where(x => x.Id == id).FirstOrDefault();
        }

        public override bool Exist(int id)
        {
            return _context.ProductOrigins.Any(x => x.Id == id);
        }
    }
}
