using APIWEB.Models;
using GraphQL.Types;

namespace APIWEB.GraphQL
{

    /// <summary>
    /// Qual entidade sera mapeada para nosso Type
    /// </summary>
    public class CategoriaType : ObjectGraphType<Categoria>
    {

        public CategoriaType()
        {
            //campos do Type
            Field(x => x.CategoriaId);
            Field(x => x.Nome);
            Field(x => x.ImagemUrl);
            Field<ListGraphType<CategoriaType>>("categorias");
        }


    }
}
