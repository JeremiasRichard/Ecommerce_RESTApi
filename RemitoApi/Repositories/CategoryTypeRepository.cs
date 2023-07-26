using RemitoApi.DataBase;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Repositories
{
    public class CategoryTypeRepository : ICategoryType
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(CategoryType categoryType)
        {
            _dbContext.Add(categoryType);
            return Save();
        }

        public bool Exist(int id)
        {
            return _dbContext.CategoryTypes.Any(x => x.Id == id);
        }

        public ICollection<CategoryType> GetAll()
        {
            return _dbContext.CategoryTypes.ToList();
        }

        public CategoryType GetById(int id)
        {
           return _dbContext.CategoryTypes.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Remove(CategoryType categoryType)
        {
            _dbContext.Remove(categoryType);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }

        public bool Update(CategoryType categoryType)
        {
           _dbContext.Update(categoryType);
            return Save();
        }
    }
}
