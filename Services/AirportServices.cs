using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Travel.Models;

namespace Travel.Services;

public class AirportServices
{
    private readonly HttpClient Client;
    public AirportServices()
    {
        Client = new HttpClient();
    }

    List<Airport>? airports = new();
    public async Task<List<Airport>?> GetAirportsAsync(string url)
    {
        if (airports?.Count > 0)
        {
            return airports;
        }

        var response = await Client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            airports = await response.Content.ReadFromJsonAsync<List<Airport>>();

            foreach (var item in airports!)
            {
                item.ISO2 = await GetCountryCodeAsync($"https://countrycode.dev/api/countries/{item.country?.Replace(" ","%20")}");
            }

            Debug.Write(airports.Count);

            return airports;
        }

        return null;
    }

    public async Task<string?> GetCountryCodeAsync(string url)
    {
        var response = await Client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var array = JsonConvert.DeserializeObject<JArray>(json);

            foreach (JObject item in array!)
            {
                return item["ISO2"]?.ToString();

            }

        }
        return null;
    }
}

