namespace Api.Repository.Interfaces;
public interface IProdutoRepository
{
    Task<Produto[]?> GetAllProdutosAync();
    Task<Produto?> GetByIdProduto(int IdProduto);
    Task<Produto?> GetByNomeProduto(string Nome);
}
