using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        public Category GetByName(string name);
    }
}
