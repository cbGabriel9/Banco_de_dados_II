using Aula02.Models;
using Aula02.Repository;
using Microsoft.AspNetCore.Mvc;


namespace Aula02.Controllers
{
    public class StudentCoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentCoursesRepository _studentCoursesRepository;

        public StudentCoursesController(ICourseRepository courseRepository, IStudentRepository studentRepository, IStudentCoursesRepository studentCoursesRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _studentCoursesRepository = studentCoursesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _studentCoursesRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var allStudents = _studentRepository.GetAll();
            var allCourses = _courseRepository.GetAll();



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCourses studentCourses)
        {
            if (ModelState.IsValid)
            {
                await _studentCoursesRepository.Create(studentCourses);
                return RedirectToAction("Index");
            }
            return View(studentCourses);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var course = await _courseRepository.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Course course)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            if (id.Value != course.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _courseRepository.Update(course);
                return RedirectToAction("Index");
            }

            return View(course);
        }
    }
}
