namespace Products.Models.DTO
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }

        public Guid SellerId { get; set; }
        public Guid CategoryId { get; set; }

        //Navigation Property
        public Seller Seller { get; set; }
        public Category Category { get; set; }
    }
}
