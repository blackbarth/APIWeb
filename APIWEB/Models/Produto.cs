using System;
using System.ComponentModel.DataAnnotations;

namespace APIWEB.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(80)]
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
    }
}
