using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Domain.Entities;
using ReDoMusic.Persistence.Contexts;
using ReDoMusic.Shared.Services;

namespace ReDoMusic.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        // This two services experimental services for dependency injection topic.
        private readonly RequestCountService _requestService;
        private readonly GuidGeneratorService _guidGeneratorService;

        public BrandController(RequestCountService requestService, GuidGeneratorService guidGeneratorService)
        {
            _requestService = requestService;
            _guidGeneratorService = guidGeneratorService;
            _dbContext = new();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var brands = _dbContext.Brands.ToList();

            _requestService.RequestCount += 1;

            // Experimenting DI
            Guid guid = _guidGeneratorService.Generate();

            return View(brands);
        }

        [HttpGet]
        public IActionResult Add()
        {
            _requestService.RequestCount += 1;

            return View();
        }

        [HttpPost]
        public IActionResult Add(string brandName, string brandDisplayingText, string brandAddress)
        {
            var brand = new Brand()
            {
                Name = brandName,
                DisplayingText = brandDisplayingText,
                Address = brandAddress,
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
            };

            _dbContext.Brands.Add(brand);

            _dbContext.SaveChanges();
            
            _requestService.RequestCount += 1;

            return View();
        }

        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            _dbContext.Brands.Remove(brand);

            _dbContext.SaveChanges();
            
            _requestService.RequestCount += 1;

            return RedirectToAction("index");
        }
    }
}
