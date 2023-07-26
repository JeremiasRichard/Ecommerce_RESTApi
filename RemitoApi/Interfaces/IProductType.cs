using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface IProductType
    {
        bool Create(ProductType productType);
        bool Update(ProductType productType);
        bool Remove(ProductType productType);
        bool Exist(int id);
        bool Save();
        public ProductType GetById(int id);
        public ICollection<ProductType> GetAll();
    }
}
