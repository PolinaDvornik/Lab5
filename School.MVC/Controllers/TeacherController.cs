using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;
using School.MVC.Models;
using System.Linq;

namespace School.MVC.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;
        private readonly IClassService _classService;
        private readonly IScheduleService _scheduleService;
        private static int _id;

        public TeacherController(ITeacherService teacherService, ISubjectService subjectService, IClassService classService, IScheduleService scheduleService)
        {
            _teacherService = teacherService;
            _subjectService = subjectService;
            _classService = classService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classes = await _classService.GetAll();

            return View(new TeacherViewModel
            {
                Teachers = teachers.ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string subject, string classNumber)
        {
            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classes = await _classService.GetAll(); 

            if (subject is not null)
            {
                teachers = teachers.Where(x => x.Position.Contains(subject));
            }

            if (classNumber is not null)
            {
                var schedules = await _scheduleService.GetAll();
                var teacherIds = schedules.Where(x => x.Class.Number.Equals(classNumber)).Select(x => x.TeacherId).Distinct();

                var idsToRemove = new List<int>();
                foreach (var item in teachers)
                {
                    bool isContains = false;

                    foreach (var id in teacherIds)
                    {
                        if (item.Id == id)
                        {
                            isContains = true;
                            break;
                        }
                    }
                    if (!isContains)
                    {
                        idsToRemove.Add(item.Id);
                    }
                }

                foreach (int id in idsToRemove)
                {
                    teachers = teachers.Where(x => x.Id != id);
                }
            }

            return View("Get", new TeacherViewModel
            {
                Teachers = teachers.ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var subjects = await _subjectService.GetAll();

            return View(new TeacherCreatedViewModel
            {
                Positions = subjects.Select(s => s.Name + " teacher").ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreatedViewModel teacherCreated)
        {
            if (!ModelState.IsValid)
            {
                var subjects1 = await _subjectService.GetAll();

                return View(new TeacherCreatedViewModel
                {
                    Positions = subjects1.Select(s => s.Name + " teacher").ToList(),
                });
            }

            await _teacherService.Create(new TeacherCreatedDto
            {
                Surname = teacherCreated.Surname,
                Name = teacherCreated.Name,
                MiddleName = teacherCreated.MiddleName,
                Position = teacherCreated.Position
            });

            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classes = await _classService.GetAll();

            return View("Get", new TeacherViewModel
            {
                Teachers = teachers.ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            _id = id;
            var subjects = await _subjectService.GetAll();
            var teacher = await _teacherService.GetById(id);

            return View(new TeacherUpdatedViewModel
            {
                Name = teacher.Name,
                Surname = teacher.Surname,
                MiddleName = teacher.MiddleName,
                Positions = subjects.Select(s => s.Name + " teacher").ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeacherUpdatedViewModel teacherUpdated)
        {
            if (!ModelState.IsValid)
            {
                var subjects1 = await _subjectService.GetAll();
                var teacher = await _teacherService.GetById(_id);

                return View(new TeacherUpdatedViewModel
                {
                    Name = teacher.Name,
                    Surname = teacher.Surname,
                    MiddleName = teacher.MiddleName,
                    Positions = subjects1.Select(s => s.Name + " teacher").ToList(),
                });
            }

            await _teacherService.Update(new TeacherUpdatedDto
            {
                Id = _id,
                Surname = teacherUpdated.Surname,
                Name = teacherUpdated.Name,
                MiddleName = teacherUpdated.MiddleName,
                Position = teacherUpdated.Position
            });

            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classes = await _classService.GetAll();

            return View("Get", new TeacherViewModel
            {
                Teachers = teachers.ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _id = id;

            return View(await _teacherService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _teacherService.Delete(_id);

            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classes = await _classService.GetAll();

            return View("Get", new TeacherViewModel
            {
                Teachers = teachers.ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
            });
        }
    }
}
