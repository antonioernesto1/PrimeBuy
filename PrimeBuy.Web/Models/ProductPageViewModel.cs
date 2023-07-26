using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.DTOs;

namespace PrimeBuy.Web.Models
{
    public class ProductPageViewModel
    {
        public ProductViewDto ProductViewDto { get; set; }
        public List<ProductViewDto> SimilarProducts { get; set; }
    }
}