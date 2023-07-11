using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.ViewModels;

namespace PrimeBuy.Web.Models
{
    public class ProductPageViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }
        public List<ProductViewModel> SimilarProducts { get; set; }
    }
}