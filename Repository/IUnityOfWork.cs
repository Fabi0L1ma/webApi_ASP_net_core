namespace WebApi.Repository
{
    public interface IUnityOfWork
    {
        ICategoriaRepository CategoriaRepository { get; }
        IProdutoRepository ProdutoRepository { get; }
        void Commit();
    }
}
