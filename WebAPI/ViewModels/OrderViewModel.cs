﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAPI.ViewModels;

namespace WebAPI.Models
{
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public List<OrderDetailViewModel> Items { get; set; } 
        ///list detail
    }


}