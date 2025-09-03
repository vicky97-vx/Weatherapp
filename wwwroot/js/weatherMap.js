window.initWeatherMap = function (lat, lon, layer, zoom) {
    if (window.weatherMap) {
        window.weatherMap.remove();
    }

    window.weatherMap = L.map('map').setView([lat, lon], zoom || 8);

    // Base map
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(window.weatherMap);

    // Weather overlay
    L.tileLayer(`https://tile.openweathermap.org/map/${layer}/{z}/{x}/{y}.png?appid=45451121afeefc545f99e695ab97fbeb`, {
        attribution: '&copy; <a href="https://openweathermap.org/">OpenWeather</a>'
    }).addTo(window.weatherMap);
};
