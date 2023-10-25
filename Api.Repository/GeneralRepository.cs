namespace Api.Repository;

public class GeneralRepository : IGeneralRepository
{
    private readonly ApiContext _context;
    public GeneralRepository(ApiContext context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }
    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }
    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }
    public void DeleteRange<T>(T[] entityarray) where T : class
    {
        _context.RemoveRange(entityarray);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
