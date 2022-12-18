using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Interfaces.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<IEnumerable<Schedule>> Get(int rowsCount, string cacheKey);
        Task<Schedule> GetById(int id);
        Task<Schedule> Create(ScheduleCreatedDto entityCreated);
        Task<bool> Delete(int id);
        Task<bool> Update(ScheduleUpdatedDto entityUpdated);
    }
}
