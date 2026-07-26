using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;

namespace WebApi.DTOs
{
    public static class ProdutoDTOMappingExtension
    {
        public static ProdutoDTO? ToProdutoDTO(this Produto produto)
        {
            if (produto == null)
            {
                return null;
            }

            var produtoDTO = new ProdutoDTO()
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                CategoriaId = produto.CategoriaId
            };

            return produtoDTO;
        }

        public static Produto? ToProduto(this ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
            {
                return null;
            }

            var produto = new Produto()
            {
                ProdutoId = produtoDTO.ProdutoId,
                Nome = produtoDTO.Nome,
                Descricao = produtoDTO.Descricao,
                Preco = produtoDTO.Preco,
                ImagemUrl = produtoDTO.ImagemUrl,
                Estoque = produtoDTO.Estoque,
                DataCadastro = produtoDTO.DataCadastro,
                CategoriaId = produtoDTO.CategoriaId

            };

            return produto;
        }

        public static IEnumerable<ProdutoDTO> ToProdutosDTOList(this IEnumerable<Produto> produtos)
        {
            if (!produtos.Any())
            {
                return new List<ProdutoDTO>();
            }

            return produtos.Select(produto => new ProdutoDTO
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                CategoriaId = produto.CategoriaId
            }).ToList();
        }
    }

}
