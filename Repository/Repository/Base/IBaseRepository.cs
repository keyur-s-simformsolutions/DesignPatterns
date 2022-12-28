namespace Repository.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    
    }
}
