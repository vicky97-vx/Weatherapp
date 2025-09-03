using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WRModel.Models
{
    public class WeatherData
    {
        public Coord Coord { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; }
        public double UVIndex { get; set; } 

    }

    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
    public class Main
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; } 

        
        
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