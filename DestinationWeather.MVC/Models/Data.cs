using Newtonsoft.Json;
using System;
using System.Numerics;
using System.Security.Principal;

namespace DestinationWeather.MVC.Models
{
	public class Data
	{
		public string? start { get; set; }
		public string? destination { get; set; }
		public string? startLat { get; set; }
		public string? startLon { get; set; }
        public string? destinationLat { get; set; }
        public string? destinationLon { get; set; }
	}


    public class ResponseData
    {
        [JsonProperty("place_id")]
        public string place_id { get; set; }
        [JsonProperty("license")]
        public string license { get; set; }
        [JsonProperty("relation")]
        public string relation { get; set; }
        [JsonProperty("osm_id")]
        public string osm_id { get; set; }
        [JsonProperty("boundingbox")]
        public string boundingbox { get; set; }
        [JsonProperty("lat")]
        public string lat { get; set; }
        [JsonProperty("lon")]
        public string lon { get; set; }
        [JsonProperty("diplay_name")]
        public string diplay_name { get; set; }
	}
}

