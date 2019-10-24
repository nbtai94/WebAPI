using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

    }
}