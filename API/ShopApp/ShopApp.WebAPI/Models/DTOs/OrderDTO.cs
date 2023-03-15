using System;
using System.Collections.Generic;

namespace ShopApp.WebAPI.Models.DTOs
{
    public class OrderDTO
    {
        public DateTime DateCreated { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool? Active { get; set; }
        public ICollection<OrderProductDTO> OrderProducts { get; set; }
    }
}
