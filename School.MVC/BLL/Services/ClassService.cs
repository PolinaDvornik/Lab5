using AutoMapper;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Services
{
    public class ClassService : IClassService // TODO: посредники между контроллерами и интерфейсами
    {
        private readonly IRepositoriesService _repositoriesService;
        private readonly IMapper _mapper;

        public ClassService(IRepositoriesService repositoriesService, IMapper mapper)
        {
            _repositoriesService = repositoriesService;
            _mapper = mapper;
        }

        public async Task<Class> Create(ClassCreatedDto entityCreated)
        {
            var entity = _mapper.Map<Class>(entityCreated);

            await _repositoriesService.ClassRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoriesService.ClassRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoriesService.ClassRepository.Delete(entity);

            return true;
        }

        public Task<IEnumerable<Class>> Get(int rowsCount, string cacheKey) =>
            _repositoriesService.ClassRepository.Get(rowsCount, cacheKey);

        public async Task<IEnumerable<Class>> GetAll() =>
            await _repositoriesService.ClassRepository.GetAll(false);

        public async Task<Class> GetById(int id) =>
            await _repositoriesService.ClassRepository.GetById(id, false);

        public async Task<bool> Update(ClassUpdatedDto entityUpdated)
        {
            var entity = await _repositoriesService.ClassRepository.GetById(entityUpdated.Id, true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityUpdated, entity);

            await _repositoriesService.ClassRepository.Update(entity);

            return true;
        }
    }
}
