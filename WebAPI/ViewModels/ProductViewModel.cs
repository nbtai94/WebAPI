using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}