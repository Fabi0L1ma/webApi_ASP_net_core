using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ProdutoRepository _produtoRepository;
        public ProdutoController(AppDbContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                var produtos = _produtoRepository.GetAll();

                if (produtos is null)
                {
                    return NotFound("Produtos não encontrados.");
                }

                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetId(int? id)
        {
            try
            {
                var produto = _produtoRepository.GetById(id.Value);

                if (produto is null)
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(produto);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest("Produto não informado.");
                }

                var produtoCriado = _produtoRepository.Create(produto);

                return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                var produtoAtualizado = _produtoRepository.Update(produto);

                return Ok(produtoAtualizado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _produtoRepository.GetById(id);

                if (produto is null)
                {
                    return NotFound("Produto não localizado.");
                }

                var produtoDeletado = _produtoRepository.Delete(id);

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }
    }
}
