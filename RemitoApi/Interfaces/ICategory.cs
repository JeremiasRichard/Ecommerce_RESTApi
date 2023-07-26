using RemitoApi.Entities;

namespace RemitoApi.Interfaces
{
    public interface ICategory
    {
        bool Create(Category category);
        bool Update(Category category);
        bool Remove(Category category);
        bool Exist(int id);
        bool Save();
        public Category GetById(int id);
        public ICollection<Category> GetAll();
    }
}
