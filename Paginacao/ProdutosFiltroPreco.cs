namespace WebApi.Paginacao
{
    public class ProdutosFiltroPreco : QueryStringParametros
    {
        public decimal? Preco {  get; set; }
        public string? PrecoCriterio { get; set; }
    }
}
