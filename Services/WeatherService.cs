using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WRModel.Models;

namespace WSService.Services
{
    public class WeatherService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public WeatherService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["OpenWeather:ApiKey"];
        }

        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error fetching weather: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return weatherData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception in GetWeatherAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<ForecastResponse?> GetForecastAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric";
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error fetching forecast: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var forecastData = JsonSerializer.Deserialize<ForecastResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return forecastData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception in GetForecastAsync: {ex.Message}");
                return null;
            }
        }
    }
}
