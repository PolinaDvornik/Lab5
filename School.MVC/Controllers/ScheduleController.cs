using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;
using School.MVC.Models;

namespace School.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly ISubjectService _subjectService;
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private static int _id;

        public ScheduleController(IScheduleService scheduleService, ISubjectService subjectService, IClassService classService, ITeacherService teacherService)
        {
            _scheduleService = scheduleService;
            _subjectService = subjectService;
            _classService = classService;
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _scheduleService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var classes = await _classService.GetAll();
            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();

            return View(new ScheduleCreatedViewModel
            {
                Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                Classes = classes.Select(c => $"{c.Number} ({c.Id})").ToList(),
                Subjects = subjects.Select(c => $"{c.Name} ({c.Id})").ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduleCreatedViewModel scheduleCreated)
        {
            if (!ModelState.IsValid)
            {
                var classes = await _classService.GetAll();
                var teachers = await _teacherService.GetAll();
                var subjects = await _subjectService.GetAll();

                return View(new ScheduleCreatedViewModel
                {
                    Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                    Classes = classes.Select(c => $"{c.Number} ({c.Id})").ToList(),
                    Subjects = subjects.Select(c => $"{c.Name} ({c.Id})").ToList(),
                });
            }

            var subject = await _subjectService.GetById(int.Parse(scheduleCreated.Subject.Split("(")[1].Substring(0, scheduleCreated.Subject.Split("(")[1].Length - 1)));
            var teacher = await _teacherService.GetById(int.Parse(scheduleCreated.Teacher.Split("(")[1].Substring(0, scheduleCreated.Teacher.Split("(")[1].Length - 1)));
            var class1 = await _classService.GetById(int.Parse(scheduleCreated.Class.Split("(")[1].Substring(0, scheduleCreated.Class.Split("(")[1].Length - 1)));

            var startTime = scheduleCreated.DateTime.TimeOfDay.ToString().Split(':');
            var endTime = scheduleCreated.DateTime.AddMinutes(45).TimeOfDay.ToString().Split(':');

            await _scheduleService.Create(new ScheduleCreatedDto
            {
                Date = scheduleCreated.DateTime,
                DayOfWeek = scheduleCreated.DateTime.DayOfWeek.ToString(),
                StartLessonTime = startTime[0] + ":" + startTime[1],
                EndLessonTime = endTime[0] + ":" + endTime[1],
                SubjectId = subject.Id,
                TeacherId = teacher.Id,
                ClassId = class1.Id,
            });

            return View("Get", await _scheduleService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            _id = id;
            var classes = await _classService.GetAll();
            var teachers = await _teacherService.GetAll();
            var subjects = await _subjectService.GetAll();
            var schedule = await _scheduleService.GetById(id);

            return View(new ScheduleUpdatedViewModel
            {
                DateTime = schedule.Date,
                Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                Classes = classes.Select(c => $"{c.Number} ({c.Id})").ToList(),
                Subjects = subjects.Select(c => $"{c.Name} ({c.Id})").ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ScheduleUpdatedViewModel scheduleUpdated)
        {
            if (!ModelState.IsValid)
            {
                var classes = await _classService.GetAll();
                var teachers = await _teacherService.GetAll();
                var subjects = await _subjectService.GetAll();
                var schedule = await _scheduleService.GetById(_id);

                return View(new ScheduleUpdatedViewModel
                {
                    DateTime = schedule.Date,
                    Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                    Classes = classes.Select(c => $"{c.Number} ({c.Id})").ToList(),
                    Subjects = subjects.Select(c => $"{c.Name} ({c.Id})").ToList(),
                });
            }

            var subject = await _subjectService.GetById(int.Parse(scheduleUpdated.Subject.Split("(")[1].Substring(0, scheduleUpdated.Subject.Split("(")[1].Length - 1)));
            var teacher = await _teacherService.GetById(int.Parse(scheduleUpdated.Teacher.Split("(")[1].Substring(0, scheduleUpdated.Teacher.Split("(")[1].Length - 1)));
            var class1 = await _classService.GetById(int.Parse(scheduleUpdated.Class.Split("(")[1].Substring(0, scheduleUpdated.Class.Split("(")[1].Length - 1)));

            var startTime = scheduleUpdated.DateTime.TimeOfDay.ToString().Split(':');
            var endTime = scheduleUpdated.DateTime.AddMinutes(45).TimeOfDay.ToString().Split(':');

            await _scheduleService.Update(new ScheduleUpdatedDto
            {
                Id = _id,
                Date = scheduleUpdated.DateTime,
                DayOfWeek = scheduleUpdated.DateTime.DayOfWeek.ToString(),
                StartLessonTime = startTime[0] + ":" + startTime[1],
                EndLessonTime = endTime[0] + ":" + endTime[1],
                SubjectId = subject.Id,
                TeacherId = teacher.Id,
                ClassId = class1.Id,
            });

            return View("Get", await _scheduleService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _id = id;

            return View(await _scheduleService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _scheduleService.Delete(_id);

            return View("Get", await _scheduleService.GetAll());
        }
    }
}
