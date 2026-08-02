using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Paginacao;
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

        [HttpGet("filtro/nome/paginacao")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasFiltroNome([FromQuery] CategoriasFiltroNome categoriasFiltroParams)
        {
            var categorias = _unityOfWork.CategoriaRepository.GetCategoriasPorFiltroNome(categoriasFiltroParams);

            return obterCategoria(categorias);
        }

        [HttpGet("paginacao")]
        public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriaParametros categoriaParametros)
        {
            var categorias = _unityOfWork.CategoriaRepository.GetCategorias(categoriaParametros);

            return obterCategoria(categorias);
        }

        private ActionResult<IEnumerable<CategoriaDTO>> obterCategoria(ListarPaginacao<Models.Categoria> categorias)
        {
            var meta_data = new
            {
                categorias.TotalItens,
                categorias.TamnhoPagina,
                categorias.NumeroPagina,
                categorias.TotalPagina,
                categorias.HasProxima,
                categorias.HasAnterior
            };

            Response.Headers.Append("X-Paginacao", JsonConvert.SerializeObject(meta_data));

            var categoriasDTO = categorias.ToCategoriaDTOList();

            return Ok(categoriasDTO);
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

            var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

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
            
            var novaCategoriaDTO = categoriaAtualizada.ToCategoriaDTO();

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

            var categoriaDTO = categoriaRemovida.ToCategoriaDTO();

            return Ok(categoriaDTO);
        }
    }
}
