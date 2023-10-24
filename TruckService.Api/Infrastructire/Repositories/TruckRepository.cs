using Microsoft.EntityFrameworkCore;
using TruckService.Api.Infrastructire.DatabaseContext;
using TruckService.Api.Model;
using TruckService.Api.Model.Interfaces;

namespace TruckService.Api.Infrastructire.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly TruckDbContext _context;

        public TruckRepository(TruckDbContext context)
        {
            _context = context;
        }

        public async Task<Truck?> GetAsync(string id)
        {
            return await _context.Trucks.FindAsync(id);
        }

        public async Task SaveAsync(Truck entity)
        {
            _context.Trucks.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Truck entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Truck>> GetAllAsync()
        {
            return await _context.Trucks.ToListAsync();
        }

        public async Task<IList<Truck>> GetFilterdAsync(IEntityFilter<Truck> filter)
        {
            IQueryable<Truck> query = _context.Set<Truck>();
            if (filter.FilterPredicate is not null)
            {
                query = filter.FilterPredicate(query);
            }

            query.OrderBy(a => a.TruckStatus).ThenBy(a => a.TruckStatus);

            if (filter.SortFunction is not null)
            {
                var orderedQuery = filter.SortFunction(query);
                if (orderedQuery is not null)
                    query = orderedQuery;
            }

            return await query.ToListAsync();
        }

        public async Task DeleteAsync(string key)
        {
            var truck = await _context.Trucks.FindAsync(key);
            if (truck is not null)
            {
                _context.Remove(truck);
                await _context.SaveChangesAsync();
            }
        }
    }
}
