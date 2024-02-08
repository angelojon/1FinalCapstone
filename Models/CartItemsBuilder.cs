using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1FinalCapstone.Models
{
    public class CartItemsBuilder
    {
      
        public int? CartBuilderId { get; set; } // Use int? if the corresponding column in the database is nullable
        public string Email { get; set; } // Use int? if the corresponding column in the database is nullable
        public int? BuilderId { get; set; }
        public string BuilderName { get; set; }

        public int? BuilderPrice { get; set; }

        public int? QuantityBuilder { get; set; }
        public int? SubtotalBuilder { get; set; }

        public byte[] Budimage1 { get; set; }
        public BikeBuilder ImageBuild { get; set; } // Reference to the ProductsImage model to hold the product image
    }
}