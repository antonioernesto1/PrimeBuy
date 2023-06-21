using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PrimeBuy.Entities.Models
{
    public class Customer : IdentityUser
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public List<Order> Orders { get; set; }
    }
}