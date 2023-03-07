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
        public double place_id { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string display_name { get; set; }
	}
}

