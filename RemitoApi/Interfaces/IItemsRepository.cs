using RemitoApi.Models;

namespace RemitoApi.Interfaces
{
    public interface IItemsRepository : IGenericRepository<Items, int>, ITransactionalRepository
    {

    }
}
