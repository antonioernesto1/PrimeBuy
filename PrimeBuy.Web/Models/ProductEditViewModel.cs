using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Application.ViewModels;

namespace PrimeBuy.Web.Models
{
    public class ProductEditViewModel
    {
        public ProductInputModel ProductInputModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
    }
}