using Adoption_EF_TA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Adoption_EF_TA.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        AdoptionDbContext _context = new AdoptionDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Animal> animals = _context.Animals.ToList();
            List<Animal> species = animals.DistinctBy(a=>a.Species).ToList();
            return View(species);
        }
        public IActionResult Species(string species)
        {
            List<Animal> animal = _context.Animals.Where(a=> a.Species == species).ToList();
            List<Animal> breeds = animal.DistinctBy(a=>a.Breed).ToList();
            return View(breeds);
        }
        public IActionResult Breed(string breed)
        {
            List<Animal> breeds = _context.Animals.Where(a => a.Breed == breed).ToList();
            return View(breeds);
        }
        public IActionResult Adoption(int id)
        {
            Animal animal = _context.Animals.FirstOrDefault(a=>a.Id == id);
            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return View(animal);
        }
        public IActionResult Add()
        {
            
            return View();
        }
        public IActionResult AddAnimal(Animal a)
        {
            _context.Animals.Add(a);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
    }
}