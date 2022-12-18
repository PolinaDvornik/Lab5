using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface IClassTypeRepository
    {
        Task<IEnumerable<ClassType>> GetAll(bool trackChanges);
        Task<IEnumerable<ClassType>> Get(int rowsCount, string cacheKey);
        Task<ClassType> GetById(int id, bool trackChanges);
        Task Create(ClassType entity);
        Task Create(IEnumerable<ClassType> entities);
        Task Delete(ClassType entity);
        Task Update(ClassType entity);
    }
}
