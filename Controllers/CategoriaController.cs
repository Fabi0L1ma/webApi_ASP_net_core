using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

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
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categoria = this._context.Categorias.ToList();

            if(categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return categoria;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetId(int id)
        {
            var categoria = this._context.Categorias.FirstOrDefault(c => c.CategoriaID == id);

            if(categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return categoria;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return this._context.Categorias.Include(p => p.Produtos).ToList();
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

        [HttpPut("{id:int}")]
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
