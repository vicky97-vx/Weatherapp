using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WRModel.Models
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public string Name { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Wind
    {
        public float Speed { get; set; }
    }

    public class Sys
    {
        public string Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }
}