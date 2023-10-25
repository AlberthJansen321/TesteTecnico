namespace Api.Application;
public class ProdutoApplication : IProdutoApplication
{
    private readonly IGeneralRepository _generarepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoApplication(IGeneralRepository generarepository, IProdutoRepository produtoRepository, IMapper mapper)
    {
        _generarepository = generarepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
    public async Task<ProdutoDTO[]?> GetAllProdutosAsync()
    {
        try
        {
            var produtos = await _produtoRepository.GetAllProdutosAync();

            if (produtos == null) return null;

            var resultado = _mapper.Map<ProdutoDTO[]>(produtos);

            return resultado;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<ProdutoDTO?> GetByIdProduto(int IdProduto)
    {
        try
        {
            var produto = await _produtoRepository.GetByIdProduto(IdProduto);

            if (produto == null) return null;

            return _mapper.Map<ProdutoDTO>(produto);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<ProdutoDTO?> Add(ProdutoDTO model)
    {
        try
        {
            var produto = _mapper.Map<Produto>(model);

            _generarepository.Add(produto);

            if (await _generarepository.SaveChangesAsync())
            {
                var produto_retorno = await _produtoRepository.GetByIdProduto(produto.Id);

                return _mapper.Map<ProdutoDTO>(produto_retorno);
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> Delete(int IdProduto)
    {
        try
        {
            var produto = await _produtoRepository.GetByIdProduto(IdProduto);

            if (produto == null) return false;

            _generarepository.Delete(produto);

            return await _generarepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
  
    public async Task<ProdutoDTO?> Update(ProdutoDTO model, int IdProduto)
    {
        try
        {
            var produto = await _produtoRepository.GetByIdProduto(IdProduto);

            if (produto == null) return null;

            _mapper.Map(model, produto);
            produto.Id = IdProduto;
            _generarepository.Update(produto);

            if (await _generarepository.SaveChangesAsync())
            {
                var produto_retorno = await _produtoRepository.GetByIdProduto(IdProduto);

                return _mapper.Map<ProdutoDTO>(produto_retorno);
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
