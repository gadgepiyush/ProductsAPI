namespace Products.Models.DTO
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }

        public Guid SellerId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
