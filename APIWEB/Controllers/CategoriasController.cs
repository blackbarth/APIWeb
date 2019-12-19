using APIWEB.Models;
using APIWEB.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace APIWEB.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriasController(IUnitOfWork contexto, IConfiguration configuration, ILogger<CategoriasController> logger)
        {
            _uof = contexto;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
        }

        [HttpGet]
        public ActionResult<ICollection<Categoria>> Get()
        {
            return _uof.CategoriaRepository.Get().ToList();
        }


        [HttpGet("autor")]
        public string GetAutor()
        {
            var autor = _configuration["autor"];
            var conexao = _configuration["ConnectionStrings:DefaultConnection"];
            return $"Autor : {autor} Conexao: {conexao}";
        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        {
            _logger.LogInformation("========================== GET api/categorias/produtos ===============================");
            return _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
            _logger.LogInformation($"========================== GET api/categorias/id = {id} ===============================");
            if (categoria == null)
            {
                _logger.LogInformation($"========================== GET api/categorias/id = {id} NOT FOUND ===============================");
                return BadRequest();
            }

            return categoria;
        }

        [HttpPost]
        public ActionResult<Categoria> Post([FromBody]Categoria categoria)
        {
            _uof.CategoriaRepository.Add(categoria);
            _uof.Commit();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id}")]
        public ActionResult<Categoria> Put(int id, [FromBody]Categoria categoria)
        {
            if (id != categoria.CategoriaId) return BadRequest();

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria == null) return BadRequest();

            _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            return categoria;
        }
    }
}
