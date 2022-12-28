using Repository.Data;

namespace Repository.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _ = _context.Set<T>().Add(entity);
            _ = _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _ = _context.Set<T>().Update(entity);
            _ = _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _ = _context.Set<T>().Remove(entity);
            _ = _context.SaveChanges();
        }

    }
}
