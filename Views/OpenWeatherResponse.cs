using System.Collections.Generic;

namespace WeatherChecker
{
  internal class OpenWeatherResponse
  {
        public string Name { get; set; }

        public List<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }
    }
}