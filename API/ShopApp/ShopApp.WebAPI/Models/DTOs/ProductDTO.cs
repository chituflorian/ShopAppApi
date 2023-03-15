using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebAPI.Models.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int? Stock { get; set; }

        public ICollection<OrderProductDTO> OrderProducts { get; set; }
    }
}
