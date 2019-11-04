using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string NormalizeName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual IList<OrderDetail> OrderDetails { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
        public DateTime CreateDate { get; set; }

    }
}