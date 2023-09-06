using EcommerceRESTApi.Models;

namespace EcommerceRESTApi.Interfaces
{
    public interface IItemsRepository : IGenericRepository<Items, int>, ITransactionalRepository
    {

    }
}
