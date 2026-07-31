namespace WebApi.Paginacao
{
    public class ProdutoPaginacao
    {
        const int maxPageSize = 10;
        public int NumeroPagina { get; set; } = 1;
        private int _tamanhoPagina;

        public int TamanhoPagina
        {
            get 
            {
                return _tamanhoPagina;
            }
            set
            {
                _tamanhoPagina = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
