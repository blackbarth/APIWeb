using APIWEB.DTOs;
using APIWEB.Models;
using APIWEB.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIWEB.Controllers
{

    [Produces("application/json")] //define formato padrao da api como json
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    //[EnableCors("PermitirApiRequest")] //configuracao #02
    public class CategoriasController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork contexto, IConfiguration configuration, ILogger<CategoriasController> logger, IMapper mapper)
        {
            _uof = contexto;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
                var categoriaDTO = _mapper.Map<List<CategoriaDTO>>(categoria);
                //throw new Exception(); //utilizado na unidade de teste
                return categoriaDTO;
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [HttpGet]
        public ActionResult<ICollection<CategoriaDTO>> Get()
        {
            try
            {
                var categorias = _uof.CategoriaRepository.Get().ToList();
                var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
                //throw new Exception(); //utilizado so para teste unitario
                return categoriasDTO;

            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }


        [HttpGet("paginacao")]
        public ActionResult<ICollection<CategoriaDTO>> GetPaginacao(int pag = 1, int reg = 5)
        {
            try
            {
                var categorias = _uof.CategoriaRepository.LocalizaPagina<Categoria>(pag, reg).ToList();

                var totalDeRegistro = _uof.CategoriaRepository.GetTotalRegistros();
                var numeroPaginas = ((int)Math.Ceiling((double)totalDeRegistro / reg));

                Response.Headers["X-Total-Registro"] = totalDeRegistro.ToString();
                Response.Headers["X-Numero-Paginas"] = numeroPaginas.ToString();

                var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
                //throw new Exception(); //utilizado so para teste unitario
                return categoriasDTO;

            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("autor")]
        public string GetAutor()
        {
            var autor = _configuration["autor"];
            var conexao = _configuration["ConnectionStrings:DefaultConnection"];
            return $"Autor : {autor} Conexao: {conexao}";
        }


        //[HttpGet("produtos")]
        //public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        //{
        //    _logger.LogInformation("========================== GET api/categorias/produtos ===============================");
        //    return _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
        //}

        /// <summary>
        /// Obtem uma categoria pelo seu Id
        /// </summary>
        /// <param name="id">Codigo da Categoria</param>
        /// <returns>Objeto Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        [ProducesResponseType(typeof(CategoriaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoriaDTO> Get(int id)
        {

            var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);


            // _logger.LogInformation($"========================== GET api/categorias/id = {id} ===============================");
            if (categoria == null)
            {
                //    _logger.LogInformation($"========================== GET api/categorias/id = {id} NOT FOUND ===============================");
                return NotFound();
            }
            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);


            return categoriaDTO;
        }

        /// <summary>
        /// Inclui uma nova categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///   POST api/categorias
        ///   {
        ///     "categoriaId": 1,
        ///     "nome": "categoria1",
        ///     "imagemUrl": "http://teste.com.br/imagem.jpg"
        ///   }
        /// </remarks>
        /// <param name="categoria">Objeto da categoria</param>
        /// <returns>O objeto Categoria incluido</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoriaDTO> Post([FromBody]CategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uof.CategoriaRepository.Add(categoria);
            _uof.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDto);
        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public ActionResult<CategoriaDTO> Put(int id, [FromBody]CategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            if (id != categoria.CategoriaId) return BadRequest();

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);
            if (categoria == null) return BadRequest();

            _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;
        }
    }
}
