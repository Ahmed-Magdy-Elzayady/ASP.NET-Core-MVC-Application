using System.ComponentModel.DataAnnotations;

namespace Day04Lab.Validations
{
    public class ProductionDateValidationAttribute:ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateOnly date = (DateOnly)value!;

            if (date > DateOnly.FromDateTime(DateTime.Today))
            {
                return new ValidationResult("Production Date can not be in the fucture");
            }
            return ValidationResult.Success;
        }
    }
}
