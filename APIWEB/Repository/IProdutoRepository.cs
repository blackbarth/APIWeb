using APIWEB.Models;
using System.Collections.Generic;

namespace APIWEB.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
