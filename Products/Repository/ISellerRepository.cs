using Products.Models.Domain;

namespace Products.Repository
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAll();

        Task<Seller> GetById(Guid id);

        Task<Seller> AddSeller(Seller seller);

        Task<Seller> UpdateSeller(Guid id, Seller seller);

        Task<Seller> DeleteSeller(Guid id);
        
    }
}
