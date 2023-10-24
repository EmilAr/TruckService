using TruckService.Api.Model.Interfaces;

namespace TruckService.Api.Infrastructire.Filters
{
    public abstract class EntityFilter<T> : IEntityFilter<T> where T : IEntity
    {
        public Func<IQueryable<T>, IQueryable<T>>? FilterPredicate { get; set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>?>? SortFunction { get; set; }
    }
}
