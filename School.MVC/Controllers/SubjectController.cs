using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;
using System.Security.Cryptography;

namespace School.MVC.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        private static int _id;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _subjectService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectCreatedDto subjectCreated)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _subjectService.Create(subjectCreated);

            return View("Get", await _subjectService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            _id = id;
            var subject = await _subjectService.GetById(id);

            return View(new SubjectUpdatedDto
            {
                Name = subject.Name,
                Description = subject.Description,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubjectUpdatedDto subjectUpdated)
        {
            if (!ModelState.IsValid)
            {
                var subject = await _subjectService.GetById(_id);

                return View(new SubjectUpdatedDto
                {
                    Name = subject.Name,
                    Description = subject.Description,
                });
            }

            subjectUpdated.Id = _id;

            await _subjectService.Update(subjectUpdated);

            return View("Get", await _subjectService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _id = id;

            return View(await _subjectService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _subjectService.Delete(_id);

            return View("Get", await _subjectService.GetAll());
        }
    }
}
