using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, int>, ITransactionalRepository
    {
        public bool GetByNameOriginAndType(string productName, int originId, int typeId);
    }
}
