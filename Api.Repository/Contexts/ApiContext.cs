namespace Api.Repository.Contexts;
public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    public DbSet<Produto> Produtos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var decimalProps = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

        foreach (var property in decimalProps)
        {
            var annotations = property.GetAnnotations();
            if (annotations.Count(x => x.Name is "Relational:ColumnType" or "Precision" or "Scale") != 0) continue;
            // Or Use this line if you use older version of C#
            // if (annotations.Count(x => x.Name == "Relational:ColumnType" || x.Name == "Precision" || x.Name == "Scale") != 0) continue;
            property.SetPrecision(18);
            property.SetScale(2);
        }
    }
}

