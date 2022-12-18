using AutoMapper;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepositoriesService _repositoriesService;
        private readonly IMapper _mapper;

        public ScheduleService(IRepositoriesService repositoriesService, IMapper mapper)
        {
            _repositoriesService = repositoriesService;
            _mapper = mapper;
        }

        public async Task<Schedule> Create(ScheduleCreatedDto entityCreated)
        {
            var entity = _mapper.Map<Schedule>(entityCreated);

            await _repositoriesService.ScheduleRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoriesService.ScheduleRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoriesService.ScheduleRepository.Delete(entity);

            return true;
        }

        public Task<IEnumerable<Schedule>> Get(int rowsCount, string cacheKey) =>
            _repositoriesService.ScheduleRepository.Get(rowsCount, cacheKey);

        public async Task<IEnumerable<Schedule>> GetAll() =>
            await _repositoriesService.ScheduleRepository.GetAll(false);

        public async Task<Schedule> GetById(int id) =>
            await _repositoriesService.ScheduleRepository.GetById(id, false);

        public async Task<bool> Update(ScheduleUpdatedDto entityUpdated)
        {
            var entity = await _repositoriesService.ScheduleRepository.GetById(entityUpdated.Id, true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityUpdated, entity);

            await _repositoriesService.ScheduleRepository.Update(entity);

            return true;
        }
    }
}
