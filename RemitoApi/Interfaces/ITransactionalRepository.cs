using Microsoft.EntityFrameworkCore.Storage;

namespace RemitoApi.Interfaces
{
    public interface ITransactionalRepository
    {
        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
    }

}
