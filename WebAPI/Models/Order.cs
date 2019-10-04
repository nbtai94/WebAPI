using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Order
    {
        public Order()
        {
           //Items = new List<OrderDetail>();
        }
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        
        public decimal TotalMoney { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public virtual IList<OrderDetail> Items { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        ///list detail
    }
}