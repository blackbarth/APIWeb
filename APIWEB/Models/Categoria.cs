﻿
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APIWEB.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        [Key]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(80)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
