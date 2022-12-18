using AutoMapper;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.DAL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() // TODO: сопоставление сущностей
        {
            CreateMap<ClassCreatedDto, Class>();
            CreateMap<ClassUpdatedDto, Class>();

            CreateMap<ClassTypeCreatedDto, ClassType>();
            CreateMap<ClassTypeUpdatedDto, ClassType>();

            CreateMap<ScheduleCreatedDto, Schedule>();
            CreateMap<ScheduleUpdatedDto, Schedule>();

            CreateMap<StudentCreatedDto, Student>();
            CreateMap<StudentUpdatedDto, Student>();

            CreateMap<SubjectCreatedDto, Subject>();
            CreateMap<SubjectUpdatedDto, Subject>();

            CreateMap<TeacherCreatedDto, Teacher>();
            CreateMap<TeacherUpdatedDto, Teacher>();
        }
    }
}
