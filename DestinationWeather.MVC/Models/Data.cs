using Newtonsoft.Json;
using System;
using System.Numerics;
using System.Security.Principal;

namespace DestinationWeather.MVC.Models
{
	public class Data
	{
        List<object> data { get; set; }
        public string start { get; set; }
        public string destination { get; set; }

        public Data() 
        {
            data = new List<object>();
        }
	}


    public class ResponseData
    {
        public double place_id { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string display_name { get; set; }
    }
}

