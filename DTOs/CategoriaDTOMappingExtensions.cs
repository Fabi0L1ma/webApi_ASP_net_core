using WebApi.Models;

namespace WebApi.DTOs
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
        {
            if (categoria == null)
            {
                return null;
            }

            return new CategoriaDTO()
            {
                CategoriaID = categoria.CategoriaID,
                Nome = categoria.Nome,
                ImageUrl = categoria.ImageUrl
            };
        }

        public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO == null)
            {
                return null;
            }

            return new Categoria()
            {
                CategoriaID = categoriaDTO.CategoriaID,
                Nome = categoriaDTO.Nome,
                ImageUrl = categoriaDTO.ImageUrl
            };
        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias) 
        { 
            if(!categorias.Any())
            {
                return new List<CategoriaDTO>();
            }

            return categorias.Select(categoria => new CategoriaDTO
            {
                CategoriaID = categoria.CategoriaID,
                Nome = categoria.Nome,
                ImageUrl = categoria.ImageUrl
            }).ToList();
        }
    }
}
