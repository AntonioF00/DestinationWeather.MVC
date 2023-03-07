using DestinationWeather.MVC.Models;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Nancy.Json;
using Newtonsoft.Json.Linq;

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
            try
            {
                Dictionary<string, object> res = new();
                var StartapiUrl = $"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q={data.start}&format=json&limit=1";
                var DestinationapiUrl = $"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q={data.destination}&format=json&limit=1";
                using (var client = new HttpClient())
                {
                    var httpClient = new HttpClient();

                    var StartRequest = new HttpRequestMessage(HttpMethod.Get, StartapiUrl);
                    var DestinationRequest = new HttpRequestMessage(HttpMethod.Get, DestinationapiUrl);

                    var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");
                    var commentValue = new ProductInfoHeaderValue("(+http://www.API.com/ScraperBot.html)");

                    StartRequest.Headers.UserAgent.Add(productValue);
                    StartRequest.Headers.UserAgent.Add(commentValue);
                    DestinationRequest.Headers.UserAgent.Add(productValue);
                    DestinationRequest.Headers.UserAgent.Add(commentValue);

                    var StartResp = await httpClient.SendAsync(StartRequest);
                    var DestinationResp = await httpClient.SendAsync(DestinationRequest);

                    //var StartDatas = await StartResp.Content.ReadAsStringAsync();
                    //var DestinationDatas = await DestinationResp.Content.ReadAsStringAsync();

                    var StartDatas = new JavaScriptSerializer().Serialize(await StartResp.Content.ReadAsStringAsync());
                    var DestinationDatas = new JavaScriptSerializer().Serialize(await DestinationResp.Content.ReadAsStringAsync());

                    dynamic StartData = JValue.Parse(StartDatas);
                    dynamic DestinationData = JValue.Parse(DestinationDatas);

                    res.Add("start", data.start);
                    res.Add("destination",  data.destination);

                    foreach (dynamic d in StartData)
                    {
                        res.Add("startLat", d.lat);
                        res.Add("startLon", d.lon);
                    }

                    foreach (dynamic d in DestinationData)
                    {
                        res.Add("destinationLat", d.lat);
                        res.Add("destinationLon", d.lon);
                    }

                    return res;
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