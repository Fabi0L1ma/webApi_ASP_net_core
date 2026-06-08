using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categoria = await this._context.Categorias.ToListAsync();

                if (categoria is null)
                {
                    return NotFound("Categorias não encontrada.");
                }

                return categoria;

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetId(int id)
        {
            try
            {
                var categoria = await this._context.Categorias.FirstOrDefaultAsync(c => c.CategoriaID == id);

                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada.");
                }

                return categoria;
            }
            catch(Exception  
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            try
            {
                return await this._context.Categorias.Include(p => p.Produtos).ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if(categoria is null)
            {
                return BadRequest();
            }

            this._context.Categorias.Add(categoria);
            
            this._context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaID }, categoria);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if(id != categoria.CategoriaID)
            {
                return BadRequest();
            }

            this._context.Entry(categoria).State = EntityState.Modified;
            this._context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = this._context.Categorias.FirstOrDefault(c => c.CategoriaID == id);

            if(categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            this._context.Categorias.Remove(categoria);
            this._context.SaveChanges();

            return Ok(categoria);
        }
    }
}
