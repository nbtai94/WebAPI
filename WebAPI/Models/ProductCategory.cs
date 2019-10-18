using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}