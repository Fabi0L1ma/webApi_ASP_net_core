using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public ProdutoController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {

            var produtos = _unityOfWork.ProdutoRepository.GetAll();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetId(int? id)
        {
            var produto = _unityOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produto);
        }

        [HttpGet("produto/{idCategoria:int}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = _unityOfWork.ProdutoRepository.GetProdutosPorCategoria(idCategoria);

            if(produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }


            return Ok(produtos);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {

            if (produto is null)
            {
                return BadRequest("Produto não informado.");
            }

            var produtoCriado = _unityOfWork.ProdutoRepository.Create(produto);

            _unityOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _unityOfWork.ProdutoRepository.Update(produto);

            _unityOfWork.Commit();

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _unityOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado.");
            }

            var produtoDeletado = _unityOfWork.ProdutoRepository.Delete(produto);

            _unityOfWork.Commit();

            return Ok(produto);
        }
    }
}
