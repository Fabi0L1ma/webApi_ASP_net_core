using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using Microsoft.AspNetCore.Http;
using WebApi.Filters;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = this._categoriaRepository.GetCategorias();

                return Ok(categorias);

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetId(int id)
        {
            try
            {
                var categoria = this._categoriaRepository.GetCategoriaPorId(id);

                if(categoria is null)
                {
                    return NotFound("Categoria não encontrada.");
                }

                return Ok(categoria);
            }
            catch(Exception)  
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        //[HttpGet("Produtos")]
        //public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        //{
        //    try
        //    {
        //        return await this._context.Categorias.Include(p => p.Produtos).ToListAsync();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
        //    }
        //}

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if(categoria is null)
            {
                return BadRequest();
            }

            var categoriaCriada = this._categoriaRepository.Create(categoria);
            
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaID }, categoriaCriada);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if(id != categoria.CategoriaID)
            {
                return BadRequest();
            }

           var categoriaAtualizada = this._categoriaRepository.Update(categoria);

            return Ok(categoriaAtualizada);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = this._categoriaRepository.GetCategoriaPorId(id);

            if(categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            var categoriaRemovida = this._categoriaRepository.Delete(id);
            
            return Ok(categoriaRemovida);
        }
    }
}
