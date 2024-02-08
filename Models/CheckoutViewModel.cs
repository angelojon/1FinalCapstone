using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1FinalCapstone.Models
{
    public class CheckoutViewModel
    {


        public List<ShopAccounts> ShopAccounts { get; internal set; }
        public int CartId { get; set; }
        public string ItemName { get; set; }


        public int? ItemPrice { get; set; }

        public int? Subtotal { get; set; }
        public int UserId { get; internal set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int? ProductId { get; set; }
        public string OrderName { get; set; }

        public int? OrderPrice { get; set; }
        public string PaymentMode { get; set; }
        public string CustomerAccountNumber { get; set; }

        public int? TotalPrice { get; set; }
        public bool IsSelected { get; set; } // New property for selection


        public List<_1FinalCapstone.Models.CheckoutTable> CheckoutTable { get; set; }
        public int? totalAmount { get; set; }
        public int CheckoutId { get; set; }
        public List<CheckoutViewModel> SelectedCartItems { get; internal set; }

        public int? OrderQuantity { get; set; }
        public int BuilderId { get; internal set; }
        public string StepTitle { get; internal set; }
        public string BuilderTitle1 { get; internal set; }
        public string BuilderDescription1 { get; internal set; }
        public int Bprice1 { get; internal set; }

        public string ConcatenatedOrderNames { get; set; }
        public int CartIdToDelete { get; set; }

        public string CartIds { get; internal set; }
        public string AdditionalNote { get; set; }
    }

}