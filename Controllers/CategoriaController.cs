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
        private readonly IUnityOfWork _unityOfWork;

        public CategoriaController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            var categorias = _unityOfWork.CategoriaRepository.GetAll();

            return Ok(categorias);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetId(int id)
        {

            var categoria = _unityOfWork.CategoriaRepository.Get(C => C.CategoriaID == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoria);
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
            if (categoria is null)
            {
                return BadRequest();
            }

            var categoriaCriada = _unityOfWork.CategoriaRepository.Create(categoria);

            _unityOfWork.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaID }, categoriaCriada);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return BadRequest();
            }

            var categoriaAtualizada = _unityOfWork.CategoriaRepository.Update(categoria);
            
            _unityOfWork.Commit();

            return Ok(categoriaAtualizada);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _unityOfWork.CategoriaRepository.Get(c => c.CategoriaID == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            var categoriaRemovida = _unityOfWork.CategoriaRepository.Delete(categoria);

            _unityOfWork.Commit();

            return Ok(categoriaRemovida);
        }
    }
}
