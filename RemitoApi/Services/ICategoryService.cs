using RemitoApi.DTOs;
using RemitoApi.Entities;

namespace RemitoApi.Services
{
    public interface ICategoryService
    {
        public Category CreateCategory(CategoryCreateDTO createDTO);
        public List<CategoryToShowDTO> GetAllCategories();
        public IQueryable<Category> GetQueryable();
        public CategoryToShowDTO GetAllProductsByCategory(int categoryId);
    }
}
