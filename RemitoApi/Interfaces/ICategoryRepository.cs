using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        public Category GetByName(string name);
    }
}
