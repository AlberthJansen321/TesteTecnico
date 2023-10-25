namespace Api.Repository.Contexts;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    public DbSet<Produto> Produtos { get; set; }
}

