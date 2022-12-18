using AutoMapper;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Services
{
    public class ClassTypeService : IClassTypeService
    {
        private readonly IRepositoriesService _repositoriesService;
        private readonly IMapper _mapper;

        public ClassTypeService(IRepositoriesService repositoriesService, IMapper mapper)
        {
            _repositoriesService = repositoriesService;
            _mapper = mapper;
        }

        public  async Task<ClassType> Create(ClassTypeCreatedDto entityCreated)
        {
            var entity = _mapper.Map<ClassType>(entityCreated);

            await _repositoriesService.ClassTypeRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoriesService.ClassTypeRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoriesService.ClassTypeRepository.Delete(entity);

            return true;
        }

        public Task<IEnumerable<ClassType>> Get(int rowsCount, string cacheKey) =>
            _repositoriesService.ClassTypeRepository.Get(rowsCount, cacheKey);

        public async Task<IEnumerable<ClassType>> GetAll() =>
            await _repositoriesService.ClassTypeRepository.GetAll(false);

        public async Task<ClassType> GetById(int id) =>
            await _repositoriesService.ClassTypeRepository.GetById(id, false);

        public async Task<bool> Update(ClassTypeUpdatedDto entityUpdated)
        {
            var entity = await _repositoriesService.ClassTypeRepository.GetById(entityUpdated.Id, true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityUpdated, entity);

            await _repositoriesService.ClassTypeRepository.Update(entity);

            return true;
        }
    }
}
