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
            var viewModel = new CreateStudentCoursesViewModel();

            viewModel.Students = await _studentRepository.GetAllNotEnrolled();
            viewModel.SetCourses(await _courseRepository.GetAll()); 

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentCoursesViewModel viewModel)
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
        public async Task<IActionResult> Update(int? studentId)
        {
            if (!studentId.HasValue)
            {
                return BadRequest();
            }
            var student = await _studentRepository.GetById(studentId!.Value);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateStudentCoursesViewModel()
            {
                SelectedStudent = student
            };

            var studentCourses = await _studentCoursesRepository.GetByStudentId(studentId!.Value);

            viewModel.SetCourses(await _courseRepository.GetAll());

            foreach(var c in viewModel.Courses)
            {
                if(studentCourses.Any(sc => sc!.CourseID == c.Id))
                {
                    c.IsSelected = true;
                }
            }

            return View(viewModel);
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
