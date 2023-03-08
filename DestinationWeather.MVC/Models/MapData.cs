using static DestinationWeather.MVC.Models.WeatherData;

namespace DestinationWeather.MVC.Models
{
    public class MapData
    {
        public List<ResponseData> StartDatas { get; set; }
        public List<ResponseData> DestinationDatas { get; set; }
        public List<DayAverages> StartCityAverages { get; set;}
        public List<DayAverages> DestinationCityAverages { get; set;}
    }
}
