using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Interfaces.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task<IEnumerable<Student>> Get(int rowsCount, string cacheKey);
        Task<Student> GetById(int id);
        Task<Student> Create(StudentCreatedDto entityCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(StudentUpdatedDto entityUpdated);
    }
}
