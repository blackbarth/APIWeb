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
    public class ProdutosController : ControllerBase
    {
        private readonly APPDBContext _context;
        public ProdutosController(APPDBContext contexto)
        {
            _context = contexto;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }



        /// <summary>
        /// Aplicar restrição ao parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetAsync(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);
            if (produto == null) return NotFound();
            return await produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId) return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            if (produto == null) return NotFound();
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;

        }


    }
}
