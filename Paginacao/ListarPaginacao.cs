namespace WebApi.Paginacao
{
    public class ListarPaginacao<T> : List<T> where T : class
    {
        public int NumeroPagina { get; set; }
        public int TotalPagina { get; set; }
        public int TamnhoPagina { get; set; }
        public int TotalItens { get; set; }
        public bool HasAnterior => NumeroPagina > 1;
        public bool HasProxima => NumeroPagina < TamnhoPagina;

        public ListarPaginacao(List<T> itens, int quantidade, int numeroPagina, int tamanhoPagina)
        { 
            TotalItens = quantidade;
            TamnhoPagina = tamanhoPagina;
            NumeroPagina = numeroPagina;
            TotalPagina = (int)Math.Ceiling(quantidade / (double)tamanhoPagina);

            AddRange(itens);
        }

        public static ListarPaginacao<T> ToListaPagina(IQueryable<T> item, int numeroPagina, int tamanhoPagina)
        {
            var quantidade = item.Count();

            var itens = item.Skip((numeroPagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

            return new ListarPaginacao<T>(itens, quantidade, numeroPagina, tamanhoPagina);
        }
    }
}
