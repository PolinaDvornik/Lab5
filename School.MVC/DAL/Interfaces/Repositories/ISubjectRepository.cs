using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAll(bool trackChanges);
        Task<IEnumerable<Subject>> Get(int rowsCount, string cacheKey);
        Task<Subject> GetById(int id, bool trackChanges);
        Task Create(Subject entity);
        Task Create(IEnumerable<Subject> entities);
        Task Delete(Subject entity);
        Task Update(Subject entity);
    }
}
