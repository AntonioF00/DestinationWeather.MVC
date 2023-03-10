using static DestinationWeather.MVC.Models.WeatherData;

namespace DestinationWeather.MVC.Models
{
    public class MapData
    {
        public List<ResponseData> StartDatas { get; set; }
        public List<ResponseData> DestinationDatas { get; set; }
        public string PointName { get; set; }
        public string PointLat { get; set; }
        public string PointLon { get; set; }
        public List<DayAverages> PointCityAverages { get; set; }
        public List<DayAverages> StartCityAverages { get; set;}
        public List<DayAverages> DestinationCityAverages { get; set;}
        
    }

    public class PointData
    {
        public location City { get; set; }
        public List<DayAverages>  CityAverages { get; set; }
    }
}
