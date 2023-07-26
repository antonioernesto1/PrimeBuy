using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.DTOs;

namespace PrimeBuy.Web.Models
{
    public class ProductEditViewModel
    {
        public ProductInputDto ProductInputDto { get; set; }
        public ProductViewDto ProductViewDto { get; set; }
    }
}