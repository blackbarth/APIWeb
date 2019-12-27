using APIWEB.Repository;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWEB.GraphQL
{

    //Mapeamos os campos para uma dada consulta
    //para uma chamada no repositorio (CategoriasRepository)
    public class CategoriaQuery : ObjectGraphType
    {

        public CategoriaQuery(IUnitOfWork _context)
        {

            //este metodo vai retornar um objeto categoria

            Field<CategoriaType>("categoria",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>() { Name = "id"}),
                resolve: context => {
                    var id = context.GetArgument<int>("id");
                    return _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
                });

            Field<ListGraphType<CategoriaType>>("categorias",
                resolve: context =>
                {
                    return _context.CategoriaRepository.Get();
                });

        }
    }
}
