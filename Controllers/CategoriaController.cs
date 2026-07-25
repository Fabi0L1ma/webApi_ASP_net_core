using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Repository;
using WebApi.DTOs;

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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _unityOfWork.CategoriaRepository.GetAll();

            var categoriasDTO = categorias.ToCategoriaDTOList();

            return Ok(categoriasDTO);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> GetId(int id)
        {

            var categoria = _unityOfWork.CategoriaRepository.Get(C => C.CategoriaID == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            var categoriaDTO = categoria.ToCategoriaDTO();

            return Ok(categoriaDTO);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
            {
                return BadRequest();
            }

            var categoria = categoriaDTO.ToCategoria();

            var categoriaCriada = _unityOfWork.CategoriaRepository.Create(categoria);

            _unityOfWork.Commit();

            var novaCategoriaDTO = categoria.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDTO.CategoriaID }, novaCategoriaDTO);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.CategoriaID)
            {
                return BadRequest();
            }

            var categoria = categoriaDTO.ToCategoria();

            var categoriaAtualizada = _unityOfWork.CategoriaRepository.Update(categoria);
            
            var novaCategoriaDTO = categoria.ToCategoriaDTO();

            _unityOfWork.Commit();

            return Ok(novaCategoriaDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _unityOfWork.CategoriaRepository.Get(c => c.CategoriaID == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            var categoriaRemovida = _unityOfWork.CategoriaRepository.Delete(categoria);

            _unityOfWork.Commit();

            var categoriaDTO = categoria.ToCategoriaDTO();

            return Ok(categoriaDTO);
        }
    }
}
