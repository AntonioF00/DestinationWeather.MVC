using DestinationWeather.MVC.Models;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace DestinationWeather.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public Data Search([Bind("start, destination")] Data data)
        {
            //dotnet dev-certs https --trust
            var locationService = new GoogleLocationService();

            var startAddress = data.start;
            var destinationAddress = data.destination;

            var startPoint = locationService.GetLatLongFromAddress(startAddress);
            var destinationPoint = locationService.GetLatLongFromAddress(destinationAddress);

            var startLatitude = startPoint.Latitude;
            var startLongitude = startPoint.Longitude;

            var destinationLatitude = destinationPoint.Latitude;
            var destinationlongitude = destinationPoint.Longitude;

            return new Data() { startLat = startLatitude.ToString(),
                                startLon = startLongitude.ToString(),
                                destinationLat = destinationLatitude.ToString(),
                                destinationLon = destinationlongitude.ToString()};
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}