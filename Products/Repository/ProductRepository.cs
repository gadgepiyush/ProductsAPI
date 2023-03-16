using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models.Domain;

namespace Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public ProductRepository(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await productsDbContext.Products.Include(x=>x.Category).Include(x=>x.Seller).ToListAsync();
        }


        public async Task<Product> GetById(Guid id)
        {
            return await productsDbContext.Products.Include(x => x.Category).Include(x => x.Seller).FirstOrDefaultAsync(x => x.Id == id);
        }

            
        public async Task<Product> AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            await productsDbContext.AddAsync(product);
            await productsDbContext.SaveChangesAsync();

            return product;
        }


        public async Task<Product> DeleteProduct(Guid id)
        {
            var product = await productsDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return null;

            productsDbContext.Products.Remove(product);
            await productsDbContext.SaveChangesAsync();

            return product;
        }


        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            var existingProduct = await productsDbContext.Products.FindAsync(id);

            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.SellerId = product.SellerId;
            existingProduct.CategoryId = product.CategoryId;

            await productsDbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
