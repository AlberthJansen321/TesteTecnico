namespace Api.Repository;
public class ProdutoRepository : IProdutoRepository
{
    private readonly ApiContext _context;
    public ProdutoRepository(ApiContext context)
    { 
        _context = context;
    }
    public async Task<Produto[]?> GetAllProdutosAync()
    {
        IQueryable<Produto> query = _context.Produtos;

        query.OrderBy(x => x.Id);

        return await query.AsNoTracking().ToArrayAsync();
    }

    public async Task<Produto?> GetByIdProduto(int IdProduto)
    {
        IQueryable<Produto> query = _context.Produtos;

        return await query.FirstOrDefaultAsync(x => x.Id == IdProduto);
    }
}


