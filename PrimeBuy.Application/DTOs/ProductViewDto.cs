using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }     
        public bool isFeatured {get; set;}
        public int CategoryId { get; set; } 
        public Category Category {get; set;}
    }
}