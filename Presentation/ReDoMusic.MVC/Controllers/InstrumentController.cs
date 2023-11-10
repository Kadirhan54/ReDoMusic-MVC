using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ReDoMusic.MVC.Services;
using ReDoMusic.Persistence.Contexts;
using ReDoMusic.Shared.Interfaces;
using ReDoMusic.Shared.Services;

namespace ReDoMusic.MVC.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        private readonly PasswordGenerator _passwordGenerator;




        public InstrumentController(PasswordGenerator passwordGenerator)
        {
            _passwordGenerator = passwordGenerator;

            _dbContext = new();
        }

        public IActionResult Index() //All Instruments will be shown
        {
            var products = _dbContext.Instruments.ToList();

            var password = _passwordGenerator.Generate(12, true, true, true, true);

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var brands = _dbContext.Brands.ToList();



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
