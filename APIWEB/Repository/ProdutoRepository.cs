using APIWEB.Context;
using APIWEB.Models;
using System.Collections.Generic;
using System.Linq;

namespace APIWEB.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(APPDBContext contexto) : base(contexto)
        {

        }
        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList(); ;
        }
    }
}
