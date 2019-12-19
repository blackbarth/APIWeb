using APIWEB.Context;
using APIWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWEB.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly APPDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriasController(APPDBContext contexto, IConfiguration configuration, ILogger<CategoriasController> logger)
        {
            _context = contexto;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<ICollection<Categoria>> Get()
        {
            return _context.Categorias.AsNoTracking().ToList();
        }


        [HttpGet("autor")]
        public string GetAutor()
        {
            var autor = _configuration["autor"];
            var conexao = _configuration["ConnectionStrings:DefaultConnection"];
            return $"Autor : {autor} Conexao: {conexao}";
        }


        [HttpGet("Produtos")]
        public ActionResult<ICollection<Categoria>> GetCategoriasProdutos()
        {
            _logger.LogInformation("========================== GET api/categorias/produtos ===============================");
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
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
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id}")]
        public ActionResult<Categoria> Put(int id, [FromBody]Categoria categoria)
        {
            if (id != categoria.CategoriaId) return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria == null) return BadRequest();

            _context.Remove(categoria);
            _context.SaveChanges();
            return categoria;
        }
    }
}
