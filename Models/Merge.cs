using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1FinalCapstone.Models
{
    public class Merge
    {
        public List<_1FinalCapstone.Models.Cart> Cart { get; set; }
        public List<_1FinalCapstone.Models.CartBuilder> CartBuilder { get; set; }
       
        public List<_1FinalCapstone.Models.Product> Products { get; set; }
        public List<_1FinalCapstone.Models.CheckoutTable> CheckoutTable { get; set; }
        public List<_1FinalCapstone.Models.CompletedOrders> CompletedOrders { get; set; }

        public List<_1FinalCapstone.Models.IndexViewModel> IndexViewModel { get; set; }

        public List<_1FinalCapstone.Models.ShopAccounts> ShopAccounts { get; set; }

        public List<_1FinalCapstone.Models.ApplicationUser> ApplicationUser { get; set; }
        public List<_1FinalCapstone.Models.BikeBuilder> BikeBuilders { get; set; }
        public int BuilderId { get; set; }
      
        public string ImageProd { get; set; } // Make sure it's a string property to hold Base64 strings
        public string OrderName { get;  set; }
        public int OrderQuantity { get; set; }
        public int OrderPrice { get; set; }
        public int TotalPrice { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get;  set; }
        public string ContactNumber { get; set; }
        public string GcashAccountName { get;  set; }
        public string GcashAccountNumber { get; set; }
        public string PaymayaAccountName { get; set; }
        public string PaymayaAccountNumber { get; set; }


        public string PaymentMode { get;  set; }

        [Required(ErrorMessage = "AccountNumber is required.")]
        public string CustomerAccountNumber { get; set; }
        public string  BuilderImage { get;  set; }
        public string Subtotal { get;  set; }
        public ProductsImage CartImage { get; set; }
        public int CheckoutId { get;  set; }
        public int CartId { get; set; }
        public int CartBuilderId { get; set; }
        public int ProductId { get; set; }
        public List<int> SelectedBuilderIds { get; internal set; }
        public int SelectedBuilderId { get; internal set; }
        public int BuilderId1 { get; internal set; }
        public string StepTitle1 { get; internal set; }
        public string BuilderTitle1 { get; internal set; }
        public string BuilderDescription1 { get; internal set; }
        public string Budimage1 { get; set; }
        public int Bprice1 { get; internal set; }
        public int BuilderId2 { get; internal set; }
        public int BuilderId3 { get; internal set; }
        public int BuilderId4 { get; internal set; }
        public int BuilderId5 { get; internal set; }
        public int BuilderId6 { get; internal set; }
        public int BuilderId7 { get; internal set; }
        public int BuilderId8 { get; internal set; }
        public int BuilderId9 { get; internal set; }
        public int BuilderId10 { get; internal set; }
        public int BuilderId11 { get; internal set; }
        public int BuilderId12 { get; internal set; }
        public List<string> BuilderDescriptions { get; internal set; }
        public int TotalBprice1 { get; set; }
    }
}