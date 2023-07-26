using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface IProduct
    {
        bool Create(Product product);
        bool Update(Product product);
        bool Remove(Product product);
        bool Exist(int id);
        bool Save();
        public Product GetById(int id);
        public ICollection<Product> GetAll();
    }
}
