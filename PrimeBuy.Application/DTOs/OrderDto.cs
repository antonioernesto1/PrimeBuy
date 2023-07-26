using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.DTOs
{
    public class OrderDto
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public List<ProductOrderDto> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}