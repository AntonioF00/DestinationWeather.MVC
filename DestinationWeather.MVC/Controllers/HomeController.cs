using DestinationWeather.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Web;

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

        #region ExecuteJavascript
        public async Task<Object> SearchAsync(/*[Bind("start, destination")] Data data*/ string start, string destination)
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

                    object value = ClientScript.RegisterStartupScript(this, GetType(), "AnyValue", "sumValues(" + StartDatas + "," + DestinationDatas + ");", true);

                    return new List<object>() { StartDatas, DestinationDatas};
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}