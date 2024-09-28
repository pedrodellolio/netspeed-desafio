using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Desafio_API.Validators
{
    public class DataNaoRetroativaAttribute : ValidationAttribute
    {
        public DataNaoRetroativaAttribute()
        {
            ErrorMessage = "A data não pode ser retroativa.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime data)
            {
                if (data < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
