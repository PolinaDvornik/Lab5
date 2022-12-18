using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Interfaces.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAll();
        Task<IEnumerable<Teacher>> Get(int rowsCount, string cacheKey);
        Task<Teacher> GetById(int id);
        Task<Teacher> Create(TeacherCreatedDto entityCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(TeacherUpdatedDto entityUpdated);
    }
}
