using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface ICategoryType
    {
        bool Create(CategoryType categoryType);
        bool Update(CategoryType categoryType);
        bool Remove(CategoryType categoryType);
        bool Exist(int id);
        bool Save();
        public CategoryType GetById(int id);
        public ICollection<CategoryType> GetAll();
    }
}
