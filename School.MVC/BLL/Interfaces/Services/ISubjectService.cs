using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Interfaces.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAll();
        Task<IEnumerable<Subject>> Get(int rowsCount, string cacheKey);
        Task<Subject> GetById(int id);
        Task<Subject> Create(SubjectCreatedDto entityCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(SubjectUpdatedDto entityUpdated);
    }
}
