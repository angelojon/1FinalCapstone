using System;
using System.ComponentModel.DataAnnotations;

namespace _1FinalCapstone.Models
{
    public partial class BikeBuilder
    {
        public int BuilderId { get; set; }

        [Required(ErrorMessage = "Step Title is required")]
        public string StepTitle { get; set; }

        [Required(ErrorMessage = "Builder Title is required")]
        public string BuilderTitle1 { get; set; }

        [Required(ErrorMessage = "Builder Description is required")]
        public string BuilderDescription1 { get; set; }

        [Required(ErrorMessage = "Bprice is required")]
        [NonNegativeAndNonZero(ErrorMessage = "Bprice must be greater than 0.")]
        public int Bprice1 { get; set; }

        public byte[] BImage1 { get; set; }
        public string BuilderTitle2 { get; set; }
        public string BuilderDescription2 { get; set; }
        public Nullable<int> Bprice2 { get; set; }
        public byte[] BImage2 { get; set; }
        public string BuilderTitle3 { get; set; }
        public string BuilderDescription3 { get; set; }
        public Nullable<int> Bprice3 { get; set; }
        public byte[] BImage3 { get; set; }
        public string ArchiveStatus { get; set; }
    }
}

public class NonNegativeAndNonZeroAttributes : ValidationAttribute
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