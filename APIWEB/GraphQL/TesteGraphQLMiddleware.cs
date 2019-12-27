using APIWEB.Repository;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace APIWEB.GraphQL
{

    //é incluido no pipeline do request para processar
    //a requisicao http usando a instancia do nosso repositorio
    public class TesteGraphQLMiddleware
    {
        //instanciapara processar o request http
        private readonly RequestDelegate _next;

        //instancia do UnitOfWork
        private readonly IUnitOfWork _context;

        public TesteGraphQLMiddleware(RequestDelegate next, IUnitOfWork context)
        {
            _next = next;
            _context = context;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            //verifica se o caminho do request é /graphql
            if (httpContext.Request.Path.StartsWithSegments("/graphql"))
            {
                //tenta ler o corpo do request usando um StreamReader
                using (var stream = new StreamReader(httpContext.Request.Body))
                {
                    var query = await stream.ReadToEndAsync();

                    if (!String.IsNullOrWhiteSpace(query))
                    {
                        //um objeto schema é criado com propriedade Query
                        //definida com uma instancia do nosso contexto (repositorio)

                        var schema = new Schema
                        {
                            Query = new CategoriaQuery(_context)
                        };

                        //criamos um DocumentExecuter que 
                        //executa a consulta contra o schema e o resultado
                        //é escrito no response como JSON via WhiteResult

                        var result = await new DocumentExecuter().ExecuteAsync(options =>
                        {
                            options.Schema = schema;
                            options.Query = query;
                        });
                        await WriteResult(httpContext, result);
                    }
                }
            }
            else
            {
                await _next(httpContext);
            }

        }

        private async Task WriteResult(HttpContext httpContext, ExecutionResult result)
        {

            //metodo
            //var json = new DocumentWriter(indent: true).Write(result);
            //httpContext.Response.StatusCode = 200;
            //httpContext.Response.ContentType = "application/json";
            //await httpContext.Response.WriteAsync(json);

        }
    }
}
