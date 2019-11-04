using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizeName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual IList<Order> Orders { get; set; }
        public string CustomerCode { get; set; }
        public DateTime CreateDate { get; set; }
    }
}