using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        
        public decimal TotalMoney { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public virtual IList<OrderDetail> Items { get; set; }
    }
}