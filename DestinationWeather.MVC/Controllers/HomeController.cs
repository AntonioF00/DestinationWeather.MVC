using DestinationWeather.MVC.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ServiceStack;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Web;

namespace DestinationWeather.MVC.Controllers
{
    public class HomeController : Controller
    {

        protected string arrayStart;
        protected string arrayDestination;
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

        public async Task<Object> Search(/*[Bind("start, destination")] Data data*/ string start, string destination)
        {
            try
            {
                Dictionary<string, object> res = new();
                var StartapiUrl = $"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q={start}&format=json&limit=1";
                var DestinationapiUrl = $"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q={destination}&format=json&limit=1";
                using (var client = new HttpClient())
                {
                    var httpClient = new HttpClient();

                    var StartRequest = new HttpRequestMessage(HttpMethod.Get, StartapiUrl);
                    var DestinationRequest = new HttpRequestMessage(HttpMethod.Get, DestinationapiUrl);

                    var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");
                    var commentValue = new ProductInfoHeaderValue("(+http://www.API.com/ScraperBot.html)");

                    httpClient.DefaultRequestHeaders.UserAgent.Add(productValue);
                    httpClient.DefaultRequestHeaders.UserAgent.Add(commentValue);
                    var StartDatas = await httpClient.GetFromJsonAsync<List<ResponseData>>(StartapiUrl);
                    var DestinationDatas = await httpClient.GetFromJsonAsync<List<ResponseData>>(DestinationapiUrl);

                    var element = new List<object>()
                    {
                        StartDatas,
                        DestinationDatas
                    };
                   
                    return View();
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