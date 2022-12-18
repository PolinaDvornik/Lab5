using AutoMapper;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IRepositoriesService _repositoriesService;
        private readonly IMapper _mapper;

        public SubjectService(IRepositoriesService repositoriesService, IMapper mapper)
        {
            _repositoriesService = repositoriesService;
            _mapper = mapper;
        }

        public async Task<Subject> Create(SubjectCreatedDto entityCreated)
        {
            var entity = _mapper.Map<Subject>(entityCreated);

            await _repositoriesService.SubjectRepository.Create(entity);

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repositoriesService.SubjectRepository.GetById(id, trackChanges: false);

            if (entity == null)
            {
                return false;
            }

            await _repositoriesService.SubjectRepository.Delete(entity);

            return true;
        }

        public Task<IEnumerable<Subject>> Get(int rowsCount, string cacheKey) =>
            _repositoriesService.SubjectRepository.Get(rowsCount, cacheKey);

        public async Task<IEnumerable<Subject>> GetAll() =>
            await _repositoriesService.SubjectRepository.GetAll(false);

        public async Task<Subject> GetById(int id) =>
            await _repositoriesService.SubjectRepository.GetById(id, false);

        public async Task<bool> Update(SubjectUpdatedDto entityUpdated)
        {
            var entity = await _repositoriesService.SubjectRepository.GetById(entityUpdated.Id, true);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(entityUpdated, entity);

            await _repositoriesService.SubjectRepository.Update(entity);

            return true;
        }
    }
}
