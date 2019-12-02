using APIWEB.Context;
using APIWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public CategoriasController(APPDBContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public ActionResult<ICollection<Categoria>> Get()
        {
            return _context.Categorias.AsNoTracking().ToList();
        }



        [HttpGet("Produtos")]
        public ActionResult<ICollection<Categoria>> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
            if (categoria == null) return BadRequest();

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
