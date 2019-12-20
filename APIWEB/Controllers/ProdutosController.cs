using APIWEB.Filters;
using APIWEB.Models;
using APIWEB.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIWEB.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public ProdutosController(IUnitOfWork contexto)
        {
            _uof = contexto;
        }


        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPreco()
        {
            return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
        }
        /// <summary>
        /// Aplicaçao de filter
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _uof.ProdutoRepository.Get().ToList();
        }



        /// <summary>
        /// Aplicar restrição ao parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null) return NotFound();
            return produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId) return BadRequest();

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null) return NotFound();
            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            return produto;

        }


    }
}
