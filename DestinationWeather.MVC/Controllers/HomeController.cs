using DestinationWeather.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml;
using static DestinationWeather.MVC.Models.WeatherData;

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

                    location startCity = new location() {  CityName = datas.start};
                    location DestinationCity = new location() {  CityName = datas.destination};
                    startCity.WeatherInfo = GetWeatherInfo(startCity).Result;
                    DestinationCity.WeatherInfo = GetWeatherInfo(startCity).Result;
                    var StartCityAverages = ProcessCityData(startCity);
                    var DestinationCityAverages = ProcessCityData(DestinationCity);

                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public static async Task<OpenWeatherAPIObject> GetWeatherInfo(location city)
        {
            OpenWeatherAPIObject weather = new OpenWeatherAPIObject();
            string openWeatherKey = GetApiKey();
            string byCity = GetUrl(city.CityName, city.StateAbbrev, city.CountryCode, openWeatherKey);

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(byCity))
            using (HttpContent content = response.Content)
            {
                var cityData = await content.ReadAsStringAsync();
                weather = JsonConvert.DeserializeObject<OpenWeatherAPIObject>(cityData);
            }

            return weather;
        }

        // returns the OpenWeatherMap Api Key
        private static string GetApiKey()
        {
            string openWeatherKey = "9525a467ef22974bc25346d4bc02de69";
            return openWeatherKey;
        }

        // returns the OpenWeatherMap Url
        private static string GetUrl(string city, string state, string country, string apiKey)
        {
            var openWeatherUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city},{state},{country}&appid={apiKey}&units=imperial";
            return openWeatherUrl;
        }

        public static List<DayAverages> ProcessCityData(location city)
        {
            List<DayAverages> averages = new List<DayAverages>();
            averages = city.WeatherInfo.list.Where(x => x.dt_txt.Date != DateTime.Now.Date)
                                            .OrderBy(x => x.dt_txt.Date)
                                            .GroupBy(x => x.dt_txt.Date)
                                            .Select(x => new DayAverages()
                                            {
                                                Day = x.Key,
                                                AveTemp = Math.Round(x.Average(y => y.main.average_temp), 2),
                                                Precipitation = x.Any(y => y.rain != null && y.rain.RainAmount > 0)
                                            })
                                            .ToList();
            return averages;
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