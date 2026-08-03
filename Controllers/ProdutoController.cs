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
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFiltroPrecoAsync([FromQuery] ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = await _unityOfWork.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFiltroParams);

            return obterProdutos(produtos);
        }


        [HttpGet("paginacao")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsyncAsync([FromQuery] ProdutoParametros produtoPaginacao) 
        {
            var produtos = await _unityOfWork.ProdutoRepository.GetProdutosAsync(produtoPaginacao);

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
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync()
        {
            var produtos = await _unityOfWork.ProdutoRepository.GetAllAsync();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }

            var produtoDTO = produtos.ToProdutosDTOList();

            return Ok(produtoDTO);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetIdAsync(int? id)
        {
            var produto = await _unityOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }

            var produtoDTO = produto.ToProdutoDTO();

            return Ok(produtoDTO);
        }

        [HttpGet("produto/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoria(int idCategoria)
        {
            var produtos = await _unityOfWork.ProdutoRepository.GetProdutosPorCategoriaAsync(idCategoria);

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
        public async Task<ActionResult<ProdutoDTO>> DeleteAsync(int id)
        {
            var produto = await _unityOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

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
