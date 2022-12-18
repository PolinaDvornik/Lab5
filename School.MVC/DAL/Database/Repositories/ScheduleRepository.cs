using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using School.MVC.Areas.Identity.Data;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly SchoolContext _context;
        private readonly IMemoryCache _memoryCache;

        public ScheduleRepository(SchoolContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Create(Schedule entity)
        {
            await _context.Schedules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<Schedule> entities)
        {
            await _context.Schedules.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Schedule entity)
        {
            _context.Schedules.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Schedule>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Schedule> entities))
            {
                entities = await _context.Schedules.Include(s => s.Teacher).Include(s => s.Class).Include(s => s.Subject).Take(rowsCount).ToListAsync();
                if (entities != null)
                {
                    _memoryCache.Set(cacheKey, entities,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return entities;
        }

        public async Task<IEnumerable<Schedule>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Schedules.AsNoTracking().Include(s => s.Teacher).Include(s => s.Class).Include(s => s.Subject).ToListAsync() :
                await _context.Schedules.Include(s => s.Teacher).Include(s => s.Class).Include(s => s.Subject).ToListAsync();
        }

        public async Task<Schedule> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Schedules.AsNoTracking().Include(s => s.Teacher).Include(s => s.Class).Include(s => s.Subject).FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Schedules.Include(s => s.Teacher).Include(s => s.Class).Include(s => s.Subject).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Schedule entity)
        {
            _context.Schedules.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
