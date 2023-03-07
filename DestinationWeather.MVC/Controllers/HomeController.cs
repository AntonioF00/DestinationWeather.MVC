using DestinationWeather.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml;

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

        [HttpPost]
        public async Task<IActionResult> Search([Bind("start,destination")] Data datas)
        {
            try
            {
                var StartapiUrl = $"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q={datas.start}&format=json&limit=1";
                var DestinationapiUrl = $"http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q={datas.destination}&format=json&limit=1";
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

                    createXml(StartDatas, DestinationDatas);

                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private void createXml(List<ResponseData> startDatas, List<ResponseData> destinationDatas)
        {
            using (XmlWriter writer = XmlWriter.Create("result.xml"))
            {
                writer.WriteStartElement("Start");
                writer.WriteElementString("city", startDatas[0].display_name);
                writer.WriteElementString("lat", startDatas[0].lat.ToString());
                writer.WriteElementString("lon",  startDatas[0].lon.ToString());
                writer.WriteStartElement("Destination");
                writer.WriteElementString("city", destinationDatas[0].display_name);
                writer.WriteElementString("lat", destinationDatas[0].lat.ToString());
                writer.WriteElementString("lon",  destinationDatas[0].lon.ToString());
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}