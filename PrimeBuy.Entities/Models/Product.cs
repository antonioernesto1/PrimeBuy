using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeBuy.Entities.Models
{
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("description")]
        [Required]
        public string Description { get; set; }
        [Column("price")]
        [Required]
        public decimal Price { get; set; }
        [Column("image_path")]
        [Required]
        public string ImagePath { get; set; }
        [Column("is_featured")]
        [Required]
        public bool isFeatured { get; set; }
        public List<Order> Orders { get; set; }
    }
}