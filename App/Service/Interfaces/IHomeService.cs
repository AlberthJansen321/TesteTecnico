using App.Models;
using static Android.Graphics.ColorSpace;

namespace App.Service.Interfaces;

public interface IHomeService
{

    Task<Produto[]> GetAllProdutosAsync();
    Task<Produto> GetByIdProduto(int CodProduto);
    Task<Produto> Add(Produto model);
    Task<bool> Delete(int CodProduto);
    Task<Produto> Update(int CodProduto, Produto model);

}
