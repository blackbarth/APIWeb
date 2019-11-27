
using System.ComponentModel.DataAnnotations;

namespace APIWEB.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
    }
}
