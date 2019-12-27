using APIWEB.DTOs;
using APIWEB.Filters;
using APIWEB.Models;
using APIWEB.Repository;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIWEB.Controllers
{

    [EnableQuery] //decoracao do odata
    [ApiConventionType(typeof(DefaultApiConventions))] // convention segunda forma
    [Produces("application/json")] //define formato padrao da api como json
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        //implementacao AutoMapper
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }


        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPreco()
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDTO;
        }
        /// <summary>
        /// Exibe a relaçao de produtos
        /// </summary>
        /// <returns>Retorna uma lista de objetos Produto</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))] //aplicaçao de filter
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _uof.ProdutoRepository.Get().ToList();
            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDTO;
        }



        /// <summary>
        /// Obter um produto po seu identificador produtoId
        /// </summary>
        /// <param name="id">Codigo do produto</param>
        /// <returns>Objeto Produto</returns>
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null) return NotFound();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return produtoDTO;
        }

        [HttpPost]
        public ActionResult Post([FromBody]ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);
            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDto);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId) return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDTO);
            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null) return NotFound();
            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return produtoDTO;

        }


    }
}
