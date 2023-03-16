using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models.Domain;

namespace Products.Repository
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ProductsDbContext productsDbContext;

        public SellerRepository(ProductsDbContext productsDbContext) {
            this.productsDbContext = productsDbContext; 
        }


        public async Task<IEnumerable<Seller>> GetAll()
        {
            return await productsDbContext.Sellers.ToListAsync();
        }


        public async Task<Seller> GetById(Guid id)
        {
            return await productsDbContext.Sellers.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Seller> AddSeller(Seller seller)
        {
            seller.Id = Guid.NewGuid();

            await productsDbContext.Sellers.AddAsync(seller);
            await productsDbContext.SaveChangesAsync();

            return seller;
        }


        public async Task<Seller> DeleteSeller(Guid id)
        {
            var seller = await productsDbContext.Sellers.FirstOrDefaultAsync(x=>x.Id ==id);

            if (seller == null)
                return null;

            productsDbContext.Sellers.Remove(seller);
            await productsDbContext.SaveChangesAsync();

            return seller;
        }


        public async Task<Seller> UpdateSeller(Guid id, Seller seller)
        {
            var existingSeller = await productsDbContext.Sellers.FirstOrDefaultAsync(x => x.Id == id);

            if (existingSeller == null)
                return null;

            existingSeller.Name = seller.Name;
            existingSeller.State = seller.State;
            existingSeller.City = seller.City;

            await productsDbContext.SaveChangesAsync();

            return existingSeller;
        }
    }
}
