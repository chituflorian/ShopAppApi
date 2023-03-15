namespace ShopApp.WebAPI.Models.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
