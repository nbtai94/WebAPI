using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public DateTime CreateDate { get; set; }
    }
}