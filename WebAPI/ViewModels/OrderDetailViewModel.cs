using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.ViewModels
{
    public class OrderDetailViewModel
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public int? OrderId { get; set; }
    } 
}