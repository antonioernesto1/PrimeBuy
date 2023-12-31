using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeBuy.Entities.Models
{
    public class OrdersProducts
    {
        [Key]
        [Column("order_id")]
        public string OrderId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Amount { get; set; }
    }
}