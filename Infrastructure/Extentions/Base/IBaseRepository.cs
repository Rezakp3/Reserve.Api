namespace Infrastructure.Extentions.Base
{
    public interface IBaseRepository<T> 
    {
        public Task<bool> Insert(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(Guid id);
        public Task<T> GetById(Guid id);
        public Task<List<T>> GetAll();
    }
}
