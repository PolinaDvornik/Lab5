using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface IClassRepository // TODO: интерфейсы имеют список методов, которые должен будет реализовать класс, реализующий (наследующий) этот интерфейс
    {
        Task<IEnumerable<Class>> GetAll(bool trackChanges);
        Task<IEnumerable<Class>> Get(int rowsCount, string cacheKey);
        Task<Class> GetById(int id, bool trackChanges);
        Task Create(Class entity);
        Task Create(IEnumerable<Class> entities);
        Task Delete(Class entity);
        Task Update(Class entity);
    }
}
