namespace ShopApp.Core.Domain
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? Stock { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
