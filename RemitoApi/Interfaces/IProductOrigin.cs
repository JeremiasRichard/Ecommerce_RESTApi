using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface IProductOrigin
    {
        bool Create(ProductOrigin productOrigin);
        bool Update(ProductOrigin productOrigin);
        bool Remove(ProductOrigin productOrigin);
        bool Exist(int id);
        bool Save();
        public ProductOrigin GetById(int id);
        public ICollection<ProductOrigin> GetAll();
    }
}
