using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Floriculture.Data;
using Floriculture.Models;
using Floriculture.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Floriculture.Controllers
{
    public class HomeController : Controller
    {
            private readonly ILogger<HomeController> _logger;
            private readonly IPlantRepository _context;

            public HomeController(ILogger<HomeController> logger, IPlantRepository plantRepository)
            {
                _context = plantRepository;
                _logger = logger;
            }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _context.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plant plant)
        {
            if (ModelState.IsValid)
            {
                await _context.Create(plant);
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }
            
            var plant = await _context.GetById(id!.Value);

            if(plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Plant plant)
        {
            if(plant == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _context.Update(plant);
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var plant = await _context.GetById(id);

            if(plant == null)
            {
                return NotFound();
            }

            await _context.Delete(plant);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
