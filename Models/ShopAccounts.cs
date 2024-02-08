using System.ComponentModel.DataAnnotations;
namespace _1FinalCapstone.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ShopAccounts
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Gcash Account Name is required")]
        public string GcashAccountName { get; set; }

        [Required(ErrorMessage = "Gcash Account Number is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Gcash Account Number must be 11 characters long")]
        public string GcashAccountNumber { get; set; }

        [Required(ErrorMessage = "Paymaya Account Name is required")]
        public string PaymayaAccountName { get; set; }

        [Required(ErrorMessage = "Paymaya Account Number is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Paymaya Account Number must be 11 characters long")]
        public string PaymayaAccountNumber { get; set; }
    }
}