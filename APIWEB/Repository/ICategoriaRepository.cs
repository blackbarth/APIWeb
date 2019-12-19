using APIWEB.Models;
using System.Collections.Generic;

namespace APIWEB.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
