using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PrimeBuy.Application.ViewModels
{
    public class ProductInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [DataType(DataType.Currency)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public bool isFeatured { get; set; }
        public int CategoryId {get; set;}
    }
}