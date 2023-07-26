using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Models;
using PrimeBuy.Entities.Models;
using PrimeBuy.Application.DTOs;

namespace PrimeBuy.Web.Utils
{
    public class Cart
    {
        public List<ProductCartDto> Products { get; set; }
    }
}