using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DAL.Models;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;
using School.MVC.Models;

namespace School.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly IClassTypeService _classTypeService;
        private readonly ISubjectService _subjectService;
        private readonly IScheduleService _scheduleService;
        private static int _id;

        public StudentController(IStudentService studentService, IClassService classService, IClassTypeService classTypeService, ISubjectService subjectService, IScheduleService scheduleService)
        {
            _studentService = studentService;
            _classService = classService;
            _classTypeService = classTypeService;
            _subjectService = subjectService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentService.GetAll();
            var classes = await _classService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View(new StudentViewModel
            {
                Students = students.ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                ClassTypes = classTypes.Select(x => x.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string classNumber, string classType, int age, string subject)
        {
            var students = await _studentService.GetAll();
            var classes = await _classService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            if (classNumber is not null)
            {
                students = students.Where(x => x.Class.Number.Equals(classNumber));
            }

            if (classType is not null)
            {
                students = students.Where(x => x.Class.ClassType.Name.Equals(classType));
            }

            if (age > 4)
            {
                students = students.Where(x => (DateTime.Now.Year - x.BirthDate.Year) == age);
            }

            if (subject is not null)
            {
                var schedules = await _scheduleService.GetAll();
                var classIds = schedules.Where(x => x.Subject.Name.Equals(subject)).Select(x => x.ClassId).Distinct();

                foreach (var id in classIds)
                {
                    bool isContains = false;

                    for (int i = 0; i < students.Count(); i++)
                    {
                        if (students.ElementAt(i).ClassId == id)
                        {
                            isContains = true;
                            break;
                        }
                    }

                    if (!isContains)
                    {
                        students = students.Where(x => x.ClassId != id);
                    }
                }
            }

            return View("Get", new StudentViewModel
            {
                Students = students.ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                ClassTypes = classTypes.Select(x => x.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var classes = await _classService.GetAll();

            return View(new StudentCreatedViewModel
            {
                Classes = classes.Select(c => $"{c.Number} ({c.Id})").ToList(),
                Sexes = new List<string>() { "male", "female" }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreatedViewModel studentCreated)
        {
            if (!ModelState.IsValid)
            {
                var classes1 = await _classService.GetAll();

                return View(new StudentCreatedViewModel
                {
                    Classes = classes1.Select(c => $"{c.Number} ({c.Id})").ToList(),
                    Sexes = new List<string>() { "male", "female" }
                });
            }

            var class1 = await _classService.GetById(int.Parse(studentCreated.Class.Split("(")[1].Substring(0, studentCreated.Class.Split("(")[1].Length - 1)));

            await _studentService.Create(new StudentCreatedDto
            {
                Surname = studentCreated.Surname,
                Name = studentCreated.Name,
                MiddleName = studentCreated.MiddleName,
                Sex = studentCreated.Sex == "male" ? true : false,
                BirthDate = studentCreated.BirthDate,
                Address = studentCreated.Address,
                MotherFullName = studentCreated.MotherFullName,
                FutherFullName = studentCreated.FutherFullName,
                AdditionalInfo = studentCreated.AdditionalInfo,
                ClassId = class1.Id
            });

            var students = await _studentService.GetAll();
            var classes = await _classService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View("Get", new StudentViewModel
            {
                Students = students.ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                ClassTypes = classTypes.Select(x => x.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            _id = id;
            var classes = await _classService.GetAll();
            var student = await _studentService.GetById(id);

            return View(new StudentUpdatedViewModel
            {
                Surname = student.Surname,
                Name = student.Name,
                MiddleName = student.MiddleName,
                Address = student.Address,
                BirthDate = student.BirthDate,
                MotherFullName = student.MotherFullName,
                FutherFullName = student.FutherFullName, 
                AdditionalInfo = student.AdditionalInfo,
                Classes = classes.Select(c => $"{c.Number} ({c.Id})").ToList(),
                Sexes = new List<string>() { "male", "female" }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(StudentUpdatedViewModel studentUpdated)
        {
            if (!ModelState.IsValid)
            {
                var classes1 = await _classService.GetAll();
                var student = await _studentService.GetById(_id);

                return View(new StudentUpdatedViewModel
                {
                    Surname = student.Surname,
                    Name = student.Name,
                    MiddleName = student.MiddleName,
                    Address = student.Address,
                    BirthDate = student.BirthDate,
                    MotherFullName = student.MotherFullName,
                    FutherFullName = student.FutherFullName,
                    AdditionalInfo = student.AdditionalInfo,
                    Classes = classes1.Select(c => $"{c.Number} ({c.Id})").ToList(),
                    Sexes = new List<string>() { "male", "female" }
                });
            }

            var class1 = await _classService.GetById(int.Parse(studentUpdated.Class.Split("(")[1].Substring(0, studentUpdated.Class.Split("(")[1].Length - 1)));

            await _studentService.Update(new StudentUpdatedDto
            {
                Id = _id,
                Surname = studentUpdated.Surname,
                Name = studentUpdated.Name,
                MiddleName = studentUpdated.MiddleName,
                Sex = studentUpdated.Sex == "male" ? true : false,
                BirthDate = studentUpdated.BirthDate,
                Address = studentUpdated.Address,
                MotherFullName = studentUpdated.MotherFullName,
                FutherFullName = studentUpdated.FutherFullName,
                AdditionalInfo = studentUpdated.AdditionalInfo,
                ClassId = class1.Id
            });

            var students = await _studentService.GetAll();
            var classes = await _classService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View("Get", new StudentViewModel
            {
                Students = students.ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                ClassTypes = classTypes.Select(x => x.Name).ToList(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _id = id;

            return View(await _studentService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _studentService.Delete(_id);

            var students = await _studentService.GetAll();
            var classes = await _classService.GetAll();
            var subjects = await _subjectService.GetAll();
            var classTypes = await _classTypeService.GetAll();

            return View("Get", new StudentViewModel
            {
                Students = students.ToList(),
                Classes = classes.Select(x => x.Number).ToList(),
                Subjects = subjects.Select(x => x.Name).ToList(),
                ClassTypes = classTypes.Select(x => x.Name).ToList(),
            });
        }
    }
}
