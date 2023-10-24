using System.Linq.Expressions;
using TruckService.Api.Model;
using TruckService.Api.Model.Dtos;
using TruckService.Api.Model.Enums;

namespace TruckService.Api.Infrastructire.Filters
{
    public class TruckFilter : EntityFilter<Truck>
    {
        public TruckFilter(TruckFilterDto truckFilterDto)
        {
            FilterPredicate = (query) =>
            {
                if (truckFilterDto.TruckStatus is not null)
                {
                    query.Where(t => t.TruckStatus == truckFilterDto.TruckStatus);
                }
                if (truckFilterDto.Name is not null)
                {
                    query.Where(t => t.Name == truckFilterDto.Name);
                }
                if (truckFilterDto.Code is not null)
                {
                    query.Where(t => t.Code == truckFilterDto.Code);
                }
                if (truckFilterDto.Description is not null)
                {
                    query.Where(t => t.Description == truckFilterDto.Description);
                }

                return query;
            };

            if (truckFilterDto.OrderFilterDtos is not null && truckFilterDto.OrderFilterDtos.Any())
            {
                SortFunction = (query) =>
                {
                    var counter = 0;
                    IOrderedQueryable<Truck>? orderedQueryable = null;
                    foreach (var orderFilter in truckFilterDto.OrderFilterDtos)
                    {
                        counter++;
                        var propertyDelegate = GetPropertyDelegate(orderFilter.OrderBy);
                        if (propertyDelegate is not null)
                        {
                            orderedQueryable = orderFilter.Ascending ? query.OrderBy(propertyDelegate) : query.OrderByDescending(propertyDelegate);
                            break;
                        }
                    }
                    if (orderedQueryable is null)
                        return orderedQueryable;

                    for (var i = counter; i < truckFilterDto.OrderFilterDtos.Count; i++)
                    {
                        orderedQueryable = OrderBy(truckFilterDto.OrderFilterDtos[i], orderedQueryable);
                    }
                    return orderedQueryable;
                };
            }
        }

        private static IOrderedQueryable<Truck> OrderBy(OrderFilterDto orderFilterDto, IOrderedQueryable<Truck> query)
        {
            var propertyDelegate = GetPropertyDelegate(orderFilterDto.OrderBy);
            if (propertyDelegate != null)
            {
                return orderFilterDto.Ascending ? query.OrderBy(propertyDelegate) : query.OrderByDescending(propertyDelegate);
            }
            return query;
        }

        private static Expression<Func<Truck, object?>>? GetPropertyDelegate(OrderProperty orderProperty) => orderProperty switch
        {
            OrderProperty.TruckStatus => (Truck t) => t.TruckStatus,
            OrderProperty.Code => (Truck t) => t.Code,
            OrderProperty.Description => (Truck t) => t.Description,
            OrderProperty.Name => (Truck t) => t.Name,
            _ => null,
        };
    }
}
