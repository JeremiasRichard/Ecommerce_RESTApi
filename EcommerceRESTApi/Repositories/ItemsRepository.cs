using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using EcommerceRESTApi.DataBase;
using EcommerceRESTApi.Interfaces;
using EcommerceRESTApi.Models;
using System.Runtime.Versioning;

namespace EcommerceRESTApi.Repositories
{
    public class ItemsRepository : GenericRepository<Items, int>, IItemsRepository
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _currentTransaction;

        public ItemsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public override bool Exist(int id)
        {
            return _context.Items.Any(x => x.Id == id);
        }

        public override IQueryable<Items> GetAll()
        {
            return _context.Items.AsQueryable();
        }

        public override Items GetById(int id)
        {
            return _context.Items.Where(x => x.Id == id).FirstOrDefault();
        }
        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction == null)
            {
                return _currentTransaction = _context.Database.BeginTransaction();
            }
            return _currentTransaction;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
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
