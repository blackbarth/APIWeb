using System.ComponentModel.DataAnnotations;


namespace APIWEB.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            //logica de validação customizada

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A Primeira letra do nome do produto deve ser maiúscula");
            }
            return ValidationResult.Success;

        }
    }
}
