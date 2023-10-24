namespace TruckService.Api.Model.Interfaces
{
    public interface IEntityFilter<T> where T : IEntity
    {
        Func<IQueryable<T>, IQueryable<T>>? FilterPredicate { get; }
        Func<IQueryable<T>, IOrderedQueryable<T>?>? SortFunction { get; }
    }
}
