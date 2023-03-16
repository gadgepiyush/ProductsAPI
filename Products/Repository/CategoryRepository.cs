using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models.Domain;

namespace Products.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public CategoryRepository(ProductsDbContext productsDbContext)
        { 
            this.productsDbContext = productsDbContext;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await productsDbContext.Categories.ToListAsync();    
        }


        public async Task<Category> GetById(Guid id)
        {
            var category = await productsDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }


        public async Task<Category> AddCategory(Category category)
        {
            category.Id = Guid.NewGuid();

            await productsDbContext.AddAsync(category);
            await productsDbContext.SaveChangesAsync();

            return category;
        }


        public async Task<Category> UpdateCategory(Guid id, Category category)
        {
            var existingProduct = await productsDbContext.Categories.FindAsync(id);

            if (existingProduct == null)
                return null;

            existingProduct.Name = category.Name;

            await productsDbContext.SaveChangesAsync();

            return existingProduct;
        }


        public async Task<Category> DeleteCategory(Guid id)
        {
            var category = await productsDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return null;

            productsDbContext.Categories.Remove(category);
            await productsDbContext.SaveChangesAsync();

            return category; 
        }
    }
}
