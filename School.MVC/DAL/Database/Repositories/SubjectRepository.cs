using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using School.MVC.Areas.Identity.Data;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolContext _context;
        private readonly IMemoryCache _memoryCache;

        public SubjectRepository(SchoolContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Create(Subject entity)
        {
            await _context.Subjects.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<Subject> entities)
        {
            await _context.Subjects.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subject entity)
        {
            _context.Subjects.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Subject> entities))
            {
                entities = await _context.Subjects.Take(rowsCount).ToListAsync();
                if (entities != null)
                {
                    _memoryCache.Set(cacheKey, entities,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return entities;
        }

        public async Task<IEnumerable<Subject>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Subjects.AsNoTracking().ToListAsync() :
                await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Subjects.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Subject entity)
        {
            _context.Subjects.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
