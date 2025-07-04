using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WRModel.Models
{
    public class WeatherResponse
    {
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public string name { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public int humidity { get; set; }
        public int pressure { get; set; }
    }

    public class Weather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
    }

    public class Sys
    {
        public string country { get; set; }
        public long sunrise { get; set; }
        public long sunset { get; set; }
    }
}