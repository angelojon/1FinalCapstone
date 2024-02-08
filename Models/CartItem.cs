using _1FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1FinalCapstone.Models
{
    public class CartItem
    {
        public int? CartId { get; set; }
        public string Email { get; set; }
        public int? ProductId { get; set; }
        public string ItemName { get; set; }
        public int? ItemPrice { get; set; }
        public int? Quantity { get; set; }
        public int? ItemQuantity { get; set; }
        public int? Subtotal { get; set; }
        public ProductsImage CartImage { get; set; } // Reference to the ProductsImage model to hold the product image
        public int UserId { get; internal set; }

        public string OrderName { get; set; }
        public int? OrderQuantity { get; set; }
        public int? OrderPrice { get; set; }
        public int? TotalPrice { get; set; }
    }
}