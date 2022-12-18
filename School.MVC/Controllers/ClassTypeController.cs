using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.BLL.Services;
using School.MVC.DTO.Creation;
using School.MVC.DTO.Update;

namespace School.MVC.Controllers
{
    public class ClassTypeController : Controller
    {
        private readonly IClassTypeService _classTypeService;
        private static int _id = 0;

        public ClassTypeController(IClassTypeService classTypeService)
        {
            _classTypeService = classTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _classTypeService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClassTypeCreatedDto classTypeCreated)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _classTypeService.Create(classTypeCreated);

            return View("Get", await _classTypeService.GetAll());
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            _id = id;
            var classType = await _classTypeService.GetById(id);

            return View(new ClassTypeUpdatedDto
            {
                Name = classType.Name,
                Description = classType.Description,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClassTypeUpdatedDto classTypeUpdated)
        {
            if (!ModelState.IsValid)
            {
                var classType = await _classTypeService.GetById(_id);

                return View(new ClassTypeUpdatedDto
                {
                    Name = classType.Name,
                    Description = classType.Description,
                });
            }

            classTypeUpdated.Id = _id;

            await _classTypeService.Update(classTypeUpdated);

            return View("Get", await _classTypeService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            _id = id;

            return View(await _classTypeService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _classTypeService.Delete(_id);

            return View("Get", await _classTypeService.GetAll());
        }
    }
}
