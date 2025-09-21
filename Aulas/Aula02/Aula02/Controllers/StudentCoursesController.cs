using Aula02.Models;
using Aula02.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


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
        public async Task<IActionResult> Create()
        {
            var allStudents = await _studentRepository.GetAll();
            var allCourses = await _courseRepository.GetAll();

            var studentsSelectList = new SelectList(allStudents, "ID", "FirstMidName");
            var coursesSelectList = new SelectList(allCourses, "ID", "Name");

            ViewBag.Students = studentsSelectList;
            ViewBag.Courses = coursesSelectList;

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
        public async Task<IActionResult> Update(int? studentId, int? courseId)
        {
            if (!studentId.HasValue || !courseId.HasValue)
            {
                return BadRequest();
            }
            var studentCourseId = await _studentCoursesRepository.Get(studentId!.Value, courseId!.Value);


            if (studentCourseId == null)
            {
                return NotFound();
            }

            var allCourses = await _courseRepository.GetAll();

            var courseSelectList = new SelectList(allCourses, "ID", "Name", courseId.Value);

            ViewBag.Courses = courseSelectList;

            return View(studentCourseId);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? studentId, int? courseId, StudentCourses studentCourses)
        {
            if (!studentId.HasValue || !courseId.HasValue)
            {
                return BadRequest();
            }

            if (studentId.Value != studentCourses.StudentID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _studentCoursesRepository.Update(studentId, courseId, studentCourses);
                return RedirectToAction("Index");
            }

            return View(studentCourses);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int studentId, int courseId)
        {
            var studentCourseToDelete = await _studentCoursesRepository.Get(studentId, courseId);

            if (studentCourseToDelete != null)
            {
                await _studentCoursesRepository.Delete(studentCourseToDelete);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
