//imports the following namespaces:
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WeatherChecker
{
  [Route("api/[controller]")]
  public class WeatherController : Controller
  {
    [HttpGet("[action]/{city}")]

//Make the controller action asynchronous
    public async Task<IActionResult> City(string city)
    {
      using (var client = new HttpClient())
      {
        try
        {
          //Call the OpenWeather API and ensure the call completed OK
          client.BaseAddress = new Uri("http://api.openweathermap.org");
          var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=2873e57eb6d4c243cc3925c30d5cbc96");
          response.EnsureSuccessStatusCode();



        //Get the response from OpenWeather and deserialize it into a C# object
          var stringResult = await response.Content.ReadAsStringAsync();
          Console.WriteLine(stringResult);
          var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);


        //Return a trimmed version of the weather data
          return Ok(new
          {
            Temp = rawWeather.Main.Temp,
            Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
            City = rawWeather.Name
          });
        }


        //Handle any errors raised making the call to OpenWeather
        catch (HttpRequestException httpRequestException)
        {
          return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
        }
      }
    }
  }
}