using Products.Models.Domain;

namespace Products.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task<Category> GetById(Guid id);

        Task<Category> AddCategory(Category category);

        Task<Category> UpdateCategory(Guid id, Category category);
        
        Task<Category> DeleteCategory(Guid id);
    }
}
