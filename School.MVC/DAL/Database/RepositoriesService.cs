using Microsoft.Extensions.Caching.Memory;
using School.MVC.Areas.Identity.Data;
using School.MVC.DAL.Database.Repositories;
using School.MVC.DAL.Interfaces;
using School.MVC.DAL.Interfaces.Repositories;

namespace School.MVC.DAL.Database
{
    public class RepositoriesService : IRepositoriesService  // TODO: класс, в котором хранятся объекты всех репозиториев
    {
        private SchoolContext _context;
        private IMemoryCache _memoryCache;

        private IClassRepository _classRepository;
        private IClassTypeRepository _classTypeRepository;
        private IScheduleRepository _scheduleRepository;
        private IStudentRepository _studentRepository;
        private ISubjectRepository _subjectRepository;
        private ITeacherRepository _teacherRepository;

        public RepositoriesService(SchoolContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public IClassRepository ClassRepository
        {
            get
            {
                if (_classRepository == null)
                {
                    _classRepository = new ClassRepository(_context, _memoryCache);
                }
                return _classRepository;
            }
        }

        public IClassTypeRepository ClassTypeRepository
        {
            get
            {
                if (_classTypeRepository == null)
                {
                    _classTypeRepository = new ClassTypeRepository(_context, _memoryCache);
                }
                return _classTypeRepository;
            }
        }

        public IScheduleRepository ScheduleRepository
        {
            get
            {
                if (_scheduleRepository == null)
                {
                    _scheduleRepository = new ScheduleRepository(_context, _memoryCache);
                }
                return _scheduleRepository;
            }
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_context, _memoryCache);
                }
                return _studentRepository;
            }
        }

        public ITeacherRepository TeacherRepository
        {
            get
            {
                if (_teacherRepository == null)
                {
                    _teacherRepository = new TeacherRepository(_context, _memoryCache);
                }
                return _teacherRepository;
            }
        }

        public ISubjectRepository SubjectRepository
        {
            get
            {
                if (_subjectRepository == null)
                {
                    _subjectRepository = new SubjectRepository(_context, _memoryCache);
                }
                return _subjectRepository;
            }
        }
    }
}
