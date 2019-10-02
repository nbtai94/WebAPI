using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal TotalQuantity { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public List<ListItem> Items { get; set; }
        ///list detail
    }
    public class ListItem
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}