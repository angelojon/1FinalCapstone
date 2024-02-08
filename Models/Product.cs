using System;

using System.ComponentModel.DataAnnotations;

namespace _1FinalCapstone.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Item Name is required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Description is required")]
        public string ItemDesc { get; set; }

        [Required(ErrorMessage = "Item Quantity is required")]
        [NonNegativeAndNonZero(ErrorMessage = "Item Quantity must be greater than 0.")]
        public Nullable<int> ItemQuantity { get; set; }

        [Required(ErrorMessage = "Item Cost is required")]
        [NonNegativeAndNonZero(ErrorMessage = "Item Cost must be greater than 0.")]
        public Nullable<int> ItemCost { get; set; }

        [Required(ErrorMessage = "Item Price is required")]
        [NonNegativeAndNonZero(ErrorMessage = "Item Price must be greater than 0.")]
        public Nullable<int> ItemPrice { get; set; }

        public byte[] Image { get; set; }
        public string ArchiveStatus { get; set; }

        [Required(ErrorMessage = "Component is required")]
        public string ComponentType { get; set; }

        public string ImageProd { get; internal set; }
        public byte[] ExistingImageDataP { get; internal set; }
    }
}

public class NonNegativeAndNonZeroAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue)
        {
            if (intValue <= 0)
            {
                return new ValidationResult(ErrorMessage ?? "Value must be greater than 0.");
            }
        }
        return ValidationResult.Success;
    }
}
