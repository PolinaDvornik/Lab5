using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Interfaces.Services
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAll();
        Task<IEnumerable<Class>> Get(int rowsCount, string cacheKey);
        Task<Class> GetById(int id);
        Task<Class> Create(ClassCreatedDto entityCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(ClassUpdatedDto entityUpdated);
    }
}
