using Aula02.Models;
using Aula02.Repository;
using Aula02.ViewModels.SubjectsCourse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Aula02.Controllers
{
    public class SubjectsCourseController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectsCourseRepository _subjectsCourse;

        public SubjectsCourseController(ISubjectRepository subjectRepository, ICourseRepository courseRepository, ISubjectsCourseRepository subjectCourseRepository)
        {
            _subjectRepository = subjectRepository;
            _courseRepository = courseRepository;
            _subjectsCourse = subjectCourseRepository;
        }

        public async Task<IActionResult> Index() // Ele retorna a view
        {
            var data = await _courseRepository.GetAll();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateSubjectsCourseViewModel();

            viewModel.Courses = await _courseRepository.GetAllNotEnrolled();
            viewModel.SetSubjects(await _subjectRepository.GetAll());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectsCourseViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                foreach (var s in viewModel.Subjects)
                {
                    if (s.IsSelected)
                    {
                        await _subjectsCourse.Create(new Models.SubjectsCourse
                        {
                            CourseID = viewModel.CourseId,
                            SubjectID = s.Id!
                        });
                    }
                }

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? courseId)
        {
            if (!courseId.HasValue)
            {
                return BadRequest();
            }
            var course = await _courseRepository.GetById(courseId!.Value);

            if (course== null)
            {
                return NotFound();
            }

            var viewModel = new UpdateSubjectsCourseViewModel()
            {
                SelectedCourse = course
            };

            var subjectsCourse = await _subjectsCourse.GetByCourseId(courseId!.Value);

            viewModel.SetSubjects(await _subjectRepository.GetAll());

            foreach (var c in viewModel.Subjects)
            {
                if (subjectsCourse.Any(sc => sc!.CourseID == c.Id))
                {
                    c.IsSelected = true;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSubjectsCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var selectedSubjectIds = viewModel.Subjects // Crio uma lista apenas com os IDs dos cursos selecionados
                        .Where(c => c.IsSelected)
                        .Select(c => c.Id)
                        .ToList();

                    await _subjectsCourse.Update(viewModel.SelectedCourse!.ID, selectedSubjectIds); // Chamo o update do repository do SubjectsCourse

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar as alterações.");
                }
            }

            var course = await _courseRepository.GetById(viewModel.SelectedCourse!.ID);
            viewModel.SelectedCourse = course;
            viewModel.SetSubjects(await _subjectRepository.GetAll());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int courseId, int subjectId)
        {
            var subjectsCourseToDelete = await _subjectsCourse.Get(courseId, subjectId);

            if (subjectsCourseToDelete != null)
            {
                await _subjectsCourse.Delete(subjectsCourseToDelete);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
