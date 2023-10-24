namespace TruckService.Api.Model.Interfaces
{
    public interface IEntity
    { }

    public interface IEntity<T> : IEntity
    {
        T Id { get; }
    }
}
