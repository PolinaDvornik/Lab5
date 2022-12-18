using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Interfaces.Services
{
    public interface IClassTypeService
    {
        Task<IEnumerable<ClassType>> GetAll();
        Task<IEnumerable<ClassType>> Get(int rowsCount, string cacheKey);
        Task<ClassType> GetById(int id);
        Task<ClassType> Create(ClassTypeCreatedDto entityCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(ClassTypeUpdatedDto entityUpdated);
    }
}
