using DestinationWeather.MVC.Models;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

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

        public async Task<Object> SearchAsync([Bind("start, destination")] Data data)
        {
            //dotnet dev-certs https --trust
            /*var locationService = new GoogleLocationService();

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
                                destinationLon = destinationlongitude.ToString()};*/
            try
            {
                var apiUrl = @"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q=Fano&format=json&limit=1";
                using (var client = new HttpClient())
                {

                    var httpClient = new HttpClient();

                    var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                    var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");
                    var commentValue = new ProductInfoHeaderValue("(+http://www.API.com/ScraperBot.html)");

                    request.Headers.UserAgent.Add(productValue);
                    request.Headers.UserAgent.Add(commentValue);

                    var resp = await httpClient.SendAsync(request);

                    var datas = await resp.Content.ReadAsStringAsync();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}