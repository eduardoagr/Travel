using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;
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
        var AirportRresponse = await Client.GetAsync(AIRPORTS_API);
        if (AirportRresponse.IsSuccessStatusCode)
        {
            airports = await AirportRresponse.Content.ReadFromJsonAsync<List<Airport>>();

            foreach (var item in airports!)
            {
               item.ISO2 = await GetCountryCodeAsync($"https://countrycode.dev/api/countries/{item.country?.Replace(" ","%20")}");
            }
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

