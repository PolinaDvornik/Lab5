using AutoMapper;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepositoriesService _repositoriesService;
        private readonly IMapper _mapper;

        public TeacherService(IRepositoriesService repositoriesService, IMapper mapper)
        {
            _repositoriesService = repositoriesService;
            _mapper = mapper;
        }

        public async Task<Teacher> Create(TeacherCreatedDto entityCreated)
        {
            var entity = _mapper.Map<Teacher>(entityCreated);

            await _repositoriesService.TeacherRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoriesService.TeacherRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoriesService.TeacherRepository.Delete(entity);

            return true;
        }

        public Task<IEnumerable<Teacher>> Get(int rowsCount, string cacheKey) =>
            _repositoriesService.TeacherRepository.Get(rowsCount, cacheKey);

        public async Task<IEnumerable<Teacher>> GetAll() =>
            await _repositoriesService.TeacherRepository.GetAll(false);

        public async Task<Teacher> GetById(int id) =>
            await _repositoriesService.TeacherRepository.GetById(id, false);

        public async Task<bool> Update(TeacherUpdatedDto entityUpdated)
        {
            var entity = await _repositoriesService.TeacherRepository.GetById(entityUpdated.Id, true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityUpdated, entity);

            await _repositoriesService.TeacherRepository.Update(entity);

            return true;
        }
    }
}
