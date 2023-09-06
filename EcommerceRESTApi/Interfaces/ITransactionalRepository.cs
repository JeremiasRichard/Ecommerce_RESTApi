using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceRESTApi.Interfaces
{
    public interface ITransactionalRepository
    {
        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
    }

}
