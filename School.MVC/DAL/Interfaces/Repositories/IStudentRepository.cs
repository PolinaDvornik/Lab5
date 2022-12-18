using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll(bool trackChanges);
        Task<IEnumerable<Student>> Get(int rowsCount, string cacheKey);
        Task<Student> GetById(int id, bool trackChanges);
        Task Create(Student entity);
        Task Create(IEnumerable<Student> entities);
        Task Delete(Student entity);
        Task Update(Student entity);
    }
}
