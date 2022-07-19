using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Travel.Models;

namespace Travel.Services;

public class AirportServices
{
    private const string AIRPORTS_API = "https://gist.githubusercontent.com/tdreyno/4278655/raw/7b0762c09b519f40397e4c3e100b097d861f5588/airports.json";
    private readonly HttpClient Client;
    public AirportServices()
    {
        Client = new HttpClient();
    }

    List<Airport>? airports = new();
    public async Task<List<Airport>?> GetAirportsAsync()
    {
        var res = await Client.GetAsync(AIRPORTS_API);
        if (res.IsSuccessStatusCode)
        {
            airports = await res.Content.ReadFromJsonAsync<List<Airport>>();

            for (var i = 0; i < airports?.Count; i++)
            {
                using var client = new HttpClient();

                var result = await client.GetAsync
                    ($"https://countrycode.dev/api/countries/{airports[i]
                    .country?.Replace(" ","%20")}");

                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();

                    var iso = string.Empty;
                    var array = JArray.Parse(response);
                    foreach (var CountryInfo in array.Children<JObject>())
                    {
                        airports[i].ISO2 = CountryInfo["ISO2"]!.ToString();
                    }
                }
            }
            return airports;
        }
        return null;
    }
}

