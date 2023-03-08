using Newtonsoft.Json;

namespace DestinationWeather.MVC.Models
{
    public class WeatherData
    {
        // day averages
        public class DayAverages
        {
            public DateTime Day { get; set; }
            public double AveTemp { get; set; }
            public bool Precipitation { get; set; }
        }

        // location
        public class location
        {
            public string CityName { get; set; }
            public string StateAbbrev { get; set; }
            public string CountryCode { get; set; }
            public OpenWeatherAPIObject WeatherInfo { get; set; }
        }
        public class OpenWeatherAPIObject
        {
            public string cod { get; set; }
            public string message { get; set; }
            public int cnt { get; set; }
            public List<List> list { get; set; }
            public City city { get; set; }
        }

        public class List
        {
            public int dt { get; set; }
            public Metrics main { get; set; }
            public List<Weather> weather { get; set; }
            public Cloud clouds { get; set; }
            public Wind wind { get; set; }
            public int visibility { get; set; }
            public float pop { get; set; }
            public Rain rain { get; set; }
            public Sys sys { get; set; }
            public DateTime dt_txt { get; set; }
        }

        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public Coord coordinate { get; set; }
            public string country { get; set; }
            public int timezone { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Coord
        {
            public float lat { get; set; }
            public float lon { get; set; }
        }

        public class Sys
        {
            public string pod { get; set; }
        }

        public class Rain
        {
            [JsonProperty("3H")]
            public float RainAmount { get; set; }
        }


        public class Wind
        {
            public float speed { get; set; }
            public int deg { get; set; }
        }

        public class Cloud
        {
            public int all { get; set; }
        }

        public class Metrics
        {
            public float temp { get; set; }
            public float feels_like { get; set; }
            public float temp_min { get; set; }
            public float temp_max { get; set; }
            public float pressure { get; set; }
            public float sea_level { get; set; }
            public float grnd_level { get; set; }
            public float humidity { get; set; }
            public float temp_kf { get; set; }
            public double average_temp { get { return Math.Round((temp_max + temp_min) / 2, 2); } }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
    }
}
