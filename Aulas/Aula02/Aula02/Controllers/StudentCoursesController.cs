using Aula02.Models;
using Aula02.Repository;
using Aula02.ViewModels.StudentCourses;
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

        public async Task<IActionResult> Index() // Ele retorna a view
        {
            var data = await _studentRepository.GetAll();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new StudentCoursesViewModel();

            viewModel.Students = await _studentRepository.GetAllNotEnrolled();
            viewModel.SetCourses(await _courseRepository.GetAll()); 

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCoursesViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                foreach(var c in viewModel.Courses)
                {
                    if(c.IsSelected)
                    {
                        await _studentCoursesRepository.Create(new Models.StudentCourses
                        {
                            StudentID = viewModel.StudentId,
                            CourseID = c.Id!,
                            SignDate = DateTime.Now
                        });
                    }

                }
                
                return RedirectToAction("Index");
            }

            return View(viewModel);
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
