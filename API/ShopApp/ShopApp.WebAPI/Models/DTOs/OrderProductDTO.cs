namespace ShopApp.WebAPI.Models.DTOs
{
    public class OrderProductDTO
    {
        public int OrderProductId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }

        public OrderDTO Order { get; set; }
        public ProductDTO Product { get; set; }
    }
}
