using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Persistence.Contexts;
using ReDoMusic.Shared.Services;

namespace ReDoMusic.MVC.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        // This two services experimental services for dependency injection topic.
        private readonly RequestCountService _requestService;
        private readonly GuidGeneratorService _guidGeneratorService;

        public InstrumentController(RequestCountService requestService, GuidGeneratorService guidGeneratorService)
        {
            _requestService = requestService;
            _guidGeneratorService = guidGeneratorService;
            _dbContext = new();
        }

        public IActionResult Index() //All Instruments will be shown
        {
            var products = _dbContext.Instruments.ToList();

            _requestService.RequestCount += 1;

            // Experimenting DI
            Guid guid = _guidGeneratorService.Generate();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var brands = _dbContext.Brands.ToList();

            _requestService.RequestCount += 1;

            return View(brands);
        }

        [HttpPost]
        public IActionResult Add(string name, string description, string brandId, string price, string barcode, string pictureUrl)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(brandId)).FirstOrDefault();

            var instrument = new Domain.Entities.Instrument()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Barcode = barcode,
                Brand = brand,
                CreatedOn = DateTime.UtcNow,
                PictureUrl = pictureUrl
            };

            _dbContext.Instruments.Add(instrument);

            _dbContext.SaveChanges();

            _requestService.RequestCount += 1;

            return RedirectToAction("add");
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Inspect(string id)
        {
            var instrument = _dbContext.Instruments.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            return View(instrument);
        }
    }
}
