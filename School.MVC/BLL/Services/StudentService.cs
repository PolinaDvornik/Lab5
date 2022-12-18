using AutoMapper;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepositoriesService _repositoriesService;
        private readonly IMapper _mapper;

        public StudentService(IRepositoriesService repositoriesService, IMapper mapper)
        {
            _repositoriesService = repositoriesService;
            _mapper = mapper;
        }

        public async Task<Student> Create(StudentCreatedDto entityCreated)
        {
            var entity = _mapper.Map<Student>(entityCreated);

            await _repositoriesService.StudentRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoriesService.StudentRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoriesService.StudentRepository.Delete(entity);

            return true;
        }

        public Task<IEnumerable<Student>> Get(int rowsCount, string cacheKey) =>
            _repositoriesService.StudentRepository.Get(rowsCount, cacheKey);

        public async Task<IEnumerable<Student>> GetAll() =>
            await _repositoriesService.StudentRepository.GetAll(false);

        public async Task<Student> GetById(int id) =>
            await _repositoriesService.StudentRepository.GetById(id, false);

        public async Task<bool> Update(StudentUpdatedDto entityUpdated)
        {
            var entity = await _repositoriesService.StudentRepository.GetById(entityUpdated.Id, true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityUpdated, entity);

            await _repositoriesService.StudentRepository.Update(entity);

            return true;
        }
    }
}
