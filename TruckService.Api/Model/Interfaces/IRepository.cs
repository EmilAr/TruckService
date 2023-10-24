namespace TruckService.Api.Model.Interfaces
{
    public interface IRepository<T, S> where T : IEntity<S>
    {
        Task<T?> GetAsync(S id);
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetFilterdAsync(IEntityFilter<T> filter);
        Task DeleteAsync(S key);
    }
}
