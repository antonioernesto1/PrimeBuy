using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PrimeBuy.Application.DTOs
{
    public class ProductInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public bool isFeatured { get; set; }
        public int CategoryId {get; set;}
    }
}