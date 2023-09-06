using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, int>, ITransactionalRepository
    {
        public bool GetByNameOriginAndType(string productName, int originId, int typeId);
    }
}
