using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;
using School.MVC.Models;

namespace School.MVC.Controllers
{
    public class ClassController : Controller // TODO: классы, на которые приходят запросы из браузера
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        private readonly IClassTypeService _classTypeService;
        private readonly IScheduleService _scheduleService;
        private static int _id;

        public ClassController(IClassService classService, ITeacherService teacherService, IClassTypeService classTypeService, IScheduleService scheduleService)
        {
            _classService = classService;
            _teacherService = teacherService;
            _classTypeService = classTypeService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var classes = await _classService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View(new ClassViewModel
            {
                Classes = classes.ToList(),
                ClassTypes = classTypes.Select(c => c.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string classType, int creationYear, int studentsCount, string dayOfWeek, int lessonCount)
        {
            var classes = await _classService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            if (classType is not null)
            {
                classes = classes.Where(c => c.ClassType.Name.Equals(classType));
            }

            if (creationYear > 0)
            {
                classes = classes.Where(c => c.CreationYear == creationYear);
            }

            if (studentsCount > 0)
            {
                classes = classes.Where(c => c.StudentsCount == studentsCount);
            }

            if (dayOfWeek is not null && lessonCount > 0)
            {
                var schedules = await _scheduleService.GetAll();
                var classIds = schedules.Where(s => s.DayOfWeek.Equals(dayOfWeek)).Select(s => s.ClassId).Distinct();
                var classIdsByCountOfLessons = new List<int>();

                foreach (int id in classIds)
                {
                    if (schedules.Where(s => s.DayOfWeek.Equals(dayOfWeek) && s.ClassId == id).Count() == lessonCount)
                        classIdsByCountOfLessons.Add(id);
                }

                var idsToRemove = new List<int>();
                foreach (var item in classes)
                {
                    bool isContains = false;

                    foreach (var id in classIdsByCountOfLessons)
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

                foreach (var id in idsToRemove)
                {
                    classes = classes.Where(x => x.Id != id);
                }
            }

            return View("Get", new ClassViewModel
            {
                Classes = classes.ToList(),
                ClassTypes = classTypes.Select(c => c.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var teachers = await _teacherService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View(new ClassCreatedViewModel
            {
                Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                ClassTypes = classTypes.Select(c => $"{c.Name} ({c.Id})").ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClassCreatedViewModel classCreated)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _teacherService.GetAll();
                var classTypes1 = await _classTypeService.GetAll();

                return View(new ClassCreatedViewModel
                {
                    Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                    ClassTypes = classTypes1.Select(c => $"{c.Name} ({c.Id})").ToList()
                });
            }

            var teacher = await _teacherService.GetById(int.Parse(classCreated.Teacher.Split("(")[1].Substring(0, classCreated.Teacher.Split("(")[1].Length - 1)));
            var classType = await _classTypeService.GetById(int.Parse(classCreated.ClassType.Split("(")[1].Substring(0, classCreated.ClassType.Split("(")[1].Length - 1)));

            await _classService.Create(new ClassCreatedDto
            {
                Number = classCreated.Number,
                StudentsCount = classCreated.StudentsCount,
                CreationYear = classCreated.CreationYear,
                TeacherId = teacher.Id,
                ClassTypeId = classType.Id
            });

            var classes = await _classService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View("Get", new ClassViewModel
            {
                Classes = classes.ToList(),
                ClassTypes = classTypes.Select(c => c.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            _id = id;
            var teachers = await _teacherService.GetAll();
            var classTypes = await _classTypeService.GetAll();
            var class1 = await _classService.GetById(id);

            return View(new ClassUpdatedViewModel
            {
                Number = class1.Number,
                StudentsCount = class1.StudentsCount,
                CreationYear = class1.CreationYear,
                Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                ClassTypes = classTypes.Select(c => $"{c.Name} ({c.Id})").ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClassUpdatedViewModel classUpdated)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _teacherService.GetAll();
                var classTypes1 = await _classTypeService.GetAll();
                var class1 = await _classService.GetById(_id);

                return View(new ClassCreatedViewModel
                {
                    Number = class1.Number,
                    StudentsCount = class1.StudentsCount,
                    CreationYear = class1.CreationYear,
                    Teachers = teachers.Select(t => $"{t.Surname} {t.Name} {t.MiddleName}, {t.Position} ({t.Id})").ToList(),
                    ClassTypes = classTypes1.Select(c => $"{c.Name} ({c.Id})").ToList()
                });
            }

            var teacher = await _teacherService.GetById(int.Parse(classUpdated.Teacher.Split("(")[1].Substring(0, classUpdated.Teacher.Split("(")[1].Length - 1)));
            var classType = await _classTypeService.GetById(int.Parse(classUpdated.ClassType.Split("(")[1].Substring(0, classUpdated.ClassType.Split("(")[1].Length - 1)));

            await _classService.Update(new ClassUpdatedDto
            {
                Id = _id,
                Number = classUpdated.Number,
                StudentsCount = classUpdated.StudentsCount,
                CreationYear = classUpdated.CreationYear,
                TeacherId = teacher.Id,
                ClassTypeId = classType.Id
            });

            var classes = await _classService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View("Get", new ClassViewModel
            {
                Classes = classes.ToList(),
                ClassTypes = classTypes.Select(c => c.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _id = id;

            return View(await _classService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _classService.Delete(_id);

            var classes = await _classService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View("Get", new ClassViewModel
            {
                Classes = classes.ToList(),
                ClassTypes = classTypes.Select(c => c.Name).ToList(),
            });
        }
    }
}
