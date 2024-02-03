using Microsoft.AspNetCore.Mvc;


namespace ProshoreRentalApp.Controllers
{
    [Route("propertylistings")]
    public class PropertyListingController : Controller
    {
        private readonly IPropertyListingService _propertyListingService;

        public PropertyListingController(IPropertyListingService propertyListingService)
        {
            _propertyListingService = propertyListingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listings = _propertyListingService.GetAllListings();
            return View(listings);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var listing = _propertyListingService.GetListingById(id);

            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PropertyType,Location,Price,Features")] PropertyListingDto listingDto)
        {
            if (ModelState.IsValid)
            {
                _propertyListingService.AddListing(listingDto);
                return RedirectToAction(nameof(Index));
            }
            return View(listingDto);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var listing = _propertyListingService.GetListingById(id);

            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,PropertyType,Location,Price,Features")] PropertyListingDto listingDto)
        {
            if (id != listingDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _propertyListingService.UpdateListing(listingDto);
                return RedirectToAction(nameof(Index));
            }

            return View(listingDto);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var listing = _propertyListingService.GetListingById(id);

            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _propertyListingService.DeleteListing(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
