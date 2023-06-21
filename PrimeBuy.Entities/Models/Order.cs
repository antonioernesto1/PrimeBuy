using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeBuy.Entities.Models
{
    public class Order
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        [Column("customer_id")]
        public string CustomerId { get; set; }
        [Column("status_id")]
        public int StatusId { get; set; }
        public List<Product> Products { get; set; }
        [Column("session_id")]
        public string? SessionId {get; set;}
        public Customer Customer { get; set; }
        [Column("order_date")]
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }
    }
}