using Products.Models.Domain;

namespace Products.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(Guid id);

        Task<Product> AddProduct(Product product);

        Task<Product> UpdateProduct(Guid id, Product product);

        Task<Product> DeleteProduct(Guid id);
    }
}
