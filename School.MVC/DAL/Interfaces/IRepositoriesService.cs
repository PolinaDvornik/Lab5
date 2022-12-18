using School.MVC.DAL.Interfaces.Repositories;

namespace School.MVC.DAL.Interfaces
{
    public interface IRepositoriesService
    {
        IClassRepository ClassRepository { get; }
        IClassTypeRepository ClassTypeRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        IStudentRepository StudentRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ITeacherRepository TeacherRepository { get; }
    }
}
