
namespace Api.Application.Interfaces;

public interface IProdutoApplication
{
    Task<ProdutoDTO?> Add(ProdutoDTO model);
    Task<ProdutoDTO?> Update(ProdutoDTO model, int IdProduto);
    Task<bool> Delete(int IdProduto);
    Task<ProdutoDTO[]?> GetAllProdutosAsync();
    Task<ProdutoDTO?> GetByIdProduto(int IdProduto);
    Task<ProdutoDTO?> GetByNomeProduto(string Nome);
}
