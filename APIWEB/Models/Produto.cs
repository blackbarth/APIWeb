

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APIWEB.Validations;

namespace APIWEB.Models
{
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(80)]
        [PrimeiraLetraMaiuscula]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(250)]
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraletra = this.Nome[0].ToString();
                if (primeiraletra != primeiraletra.ToUpper())
                {
                    yield return new
                        ValidationResult("A Primeira letra do produto deve ser maiuscula",
                        new[]{
                            nameof(this.Nome)
                            });
                }

                if (this.Estoque <= 0)
                {
                    yield return new
          ValidationResult("Estoque tem que ser maior que Zero",
          new[]{
                            nameof(this.Estoque)
              });
                }
            }
        }
    }
}
