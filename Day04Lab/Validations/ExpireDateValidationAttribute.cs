using Day04Lab.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Day04Lab.Validations
{
    public class ExpireDateValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateOnly date = (DateOnly)value;

            var model = (ProductsCreateVM)validationContext.ObjectInstance;

            if (date < model.Date_Of_Production)
            {
                return new ValidationResult("Invalid Expiredate");
            }
            return ValidationResult.Success;
        }
    }
}
