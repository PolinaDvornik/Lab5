using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using School.MVC.Areas.Identity.Data;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class ClassTypeRepository : IClassTypeRepository
    {
        private readonly SchoolContext _context;
        private readonly IMemoryCache _memoryCache;

        public ClassTypeRepository(SchoolContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Create(ClassType entity)
        {
            await _context.ClassTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<ClassType> entities)
        {
            await _context.ClassTypes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ClassType entity)
        {
            _context.ClassTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClassType>> Get(int rowsCount, string cacheKey)
        {
            if(!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ClassType> entities))
            {
                entities = await _context.ClassTypes.Take(rowsCount).ToListAsync();
                if (entities != null)
                {
                    _memoryCache.Set(cacheKey, entities,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return entities;
        }

        public async Task<IEnumerable<ClassType>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.ClassTypes.AsNoTracking().ToListAsync() :
                await _context.ClassTypes.ToListAsync();
        }

        public async Task<ClassType> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.ClassTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.ClassTypes.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(ClassType entity)
        {
            _context.ClassTypes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
