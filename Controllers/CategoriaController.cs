using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using Microsoft.AspNetCore.Http;
using WebApi.Filters;

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
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            var categoria = await this._context.Categorias.ToListAsync();

            if (categoria is null)
            {
                return NotFound("Categorias não encontrada.");
            }

            return categoria;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetId(int id)
        {
            var categoria = await this._context.Categorias.FirstOrDefaultAsync(c => c.CategoriaID == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return categoria;
        }

        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            return await this._context.Categorias.Include(p => p.Produtos).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            this._context.Categorias.Add(categoria);

            await this._context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaID }, categoria);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return BadRequest();
            }

            this._context.Entry(categoria).State = EntityState.Modified;

            await this._context.SaveChangesAsync();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = this._context.Categorias.FirstOrDefault(c => c.CategoriaID == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            this._context.Categorias.Remove(categoria);

            await this._context.SaveChangesAsync();

            return Ok(categoria);
        }
    }
}
