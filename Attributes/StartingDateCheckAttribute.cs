using System;
using System.ComponentModel.DataAnnotations;

namespace StaffClient.Attributes
{
    public class StartingDateCheckAttribute : ValidationAttribute
    {
        public StartingDateCheckAttribute()
        {
            ErrorMessage = "Starting date cannot be in the future.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
