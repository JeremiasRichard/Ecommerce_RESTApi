using Microsoft.EntityFrameworkCore;
using RemitoApi.DataBase;
using RemitoApi.Interfaces;
using RemitoApi.Models;

namespace RemitoApi.Repositories
{
    public class DeliveryNoteRepository : GenericRepository<DeliveryNote, int>, IDeliveryNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryNoteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override bool Exist(int id)
        {
            return _context.DeliveryNotes.Any(x => x.Id == id);
        }

        public override IQueryable<DeliveryNote> GetAll()
        {
            return _context.DeliveryNotes
                .Include(dn => dn.ItemsProducts)
                .ThenInclude(itp => itp.Product)
                .AsQueryable();
        }

        public override DeliveryNote GetById(int id)
        {
            return _context.DeliveryNotes.Where(c => c.Id == id).Include(dn => dn.ItemsProducts).FirstOrDefault();
        }
    }
}
