using Aula02.Models;
using Aula02.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aula02.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _subjectRepository.GetAll()); // Ele passa a lista de estudantes para a View
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.Create(subject);
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var subject = await _subjectRepository.GetById(id.Value);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Subject subject)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            if (id.Value != subject.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _subjectRepository.Update(subject);
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }

            await _subjectRepository.Delete(subject);
            return RedirectToAction("Index");
        }


    }
}
