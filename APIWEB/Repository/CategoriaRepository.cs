using APIWEB.Context;
using APIWEB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIWEB.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(APPDBContext contexto) : base(contexto)
        {

        }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}
