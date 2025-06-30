using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WRModel.Models
{
    public class ForecastResponse
    {
        public List<ForecastItem> list { get; set; } = new();
    }

    public class ForecastItem
    {
        public long Dt { get; set; }  
        public MainInfo Main { get; set; } = new();
        public List<WeatherInfo> Weather { get; set; } = new();
        public WindInfo Wind { get; set; } = new();
    }

    public class MainInfo
    {
        public float Temp { get; set; }
    }

    public class WeatherInfo
    {
        public string Main { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class WindInfo
    {
        public float Speed { get; set; }
    }
}
