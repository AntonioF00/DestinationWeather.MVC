using System;
namespace DestinationWeather.MVC.Models
{
	public class Data
	{
		public string start { get; set; }
		public string destination { get; set; }
		public string startLat { get; set; }
		public string startLon { get; set; }
        public string destinationLat { get; set; }
        public string destinationLon { get; set; }

        public Data()
		{
		}
	}
}

