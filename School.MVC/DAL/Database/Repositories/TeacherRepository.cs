using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using School.MVC.Areas.Identity.Data;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolContext _context;
        private readonly IMemoryCache _memoryCache;

        public TeacherRepository(SchoolContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Create(Teacher entity)
        {
            await _context.Teachers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<Teacher> entities)
        {
            await _context.Teachers.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Teacher entity)
        {
            _context.Teachers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Teacher> entities))
            {
                entities = await _context.Teachers.Take(rowsCount).ToListAsync();
                if (entities != null)
                {
                    _memoryCache.Set(cacheKey, entities,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return entities;
        }

        public async Task<IEnumerable<Teacher>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Teachers.AsNoTracking().ToListAsync() :
                await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Teachers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Teacher entity)
        {
            _context.Teachers.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
