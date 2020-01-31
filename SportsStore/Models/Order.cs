﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever] // user can not input to HTTP request
        // it will be created automatically
        public int OrderID { get; set; } 
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [BindNever]
        public bool Shipped { get; set; }
        [Required(ErrorMessage ="Please enter the name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter address in line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage ="Please enter the city name")]
        public string CityName { get; set; }
        [Required(ErrorMessage ="Please enter the state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage ="Please enter a country name")] // validation message from System.ComponentModel
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
