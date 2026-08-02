using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.DTOs;
using WebApi.Paginacao;
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

        [HttpGet("filtro/preco/paginacao")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosFiltroPreco([FromQuery] ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = _unityOfWork.ProdutoRepository.GetProdutosFiltroPreco(produtosFiltroParams);

            return obterProdutos(produtos);
        }


        [HttpGet("paginacao")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutoParametros produtoPaginacao) 
        {
            var produtos = _unityOfWork.ProdutoRepository.GetProdutos(produtoPaginacao);

            return obterProdutos(produtos);
        }

        private ActionResult<IEnumerable<ProdutoDTO>> obterProdutos(ListarPaginacao<Models.Produto> produtos)
        {
            var meta_data = new
            {
                produtos.TotalItens,
                produtos.TamnhoPagina,
                produtos.NumeroPagina,
                produtos.TotalPagina,
                produtos.HasProxima,
                produtos.HasAnterior
            };

            Response.Headers.Append("X-Paginacao", JsonConvert.SerializeObject(meta_data));

            var produtosDTO = produtos.ToProdutosDTOList();

            return Ok(produtosDTO);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {

            var produtos = _unityOfWork.ProdutoRepository.GetAll();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }

            var produtoDTO = produtos.ToProdutosDTOList();

            return Ok(produtoDTO);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> GetId(int? id)
        {
            var produto = _unityOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }

            var produtoDTO = produto.ToProdutoDTO();

            return Ok(produtoDTO);
        }

        [HttpGet("produto/{idCategoria:int}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = _unityOfWork.ProdutoRepository.GetProdutosPorCategoria(idCategoria);

            if(produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }

            var produtosDTO = produtos.ToProdutosDTOList();

            return Ok(produtosDTO);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
            {
                return BadRequest("Produto não informado.");
            }

            var produto = produtoDTO.ToProduto();

            var produtoCriado = _unityOfWork.ProdutoRepository.Create(produto);

            _unityOfWork.Commit();

            var novoProdutoDTO = produtoCriado.ToProdutoDTO();

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest();
            }

            var produto = produtoDTO.ToProduto();

            var produtoAtualizado = _unityOfWork.ProdutoRepository.Update(produto);

            _unityOfWork.Commit();

            var ProdutoDTOAtualizado = produtoAtualizado.ToProdutoDTO();

            return Ok(ProdutoDTOAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _unityOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado.");
            }

            var produtoDeletado = _unityOfWork.ProdutoRepository.Delete(produto);

            _unityOfWork.Commit();

            var produtoDTODeletado = produtoDeletado.ToProdutoDTO();

            return Ok(produtoDTODeletado);
        }
    }
}
