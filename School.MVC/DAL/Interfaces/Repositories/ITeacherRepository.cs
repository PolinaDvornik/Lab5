using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll(bool trackChanges);
        Task<IEnumerable<Teacher>> Get(int rowsCount, string cacheKey);
        Task<Teacher> GetById(int id, bool trackChanges);
        Task Create(Teacher entity);
        Task Create(IEnumerable<Teacher> entities);
        Task Delete(Teacher entity);
        Task Update(Teacher entity);
    }
}
