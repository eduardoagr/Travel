using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;

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

    public async Task<List<Airport>?> GetAirportsAsync()
    {
        List<Airport>? airports = new();

        if (airports.Count > 0)
        {
            return airports;
        }

        var res = await Client.GetAsync(AIRPORTS_API);
        if (res.IsSuccessStatusCode)
        {
            airports = await res.Content.ReadFromJsonAsync<List<Airport>>();

            var countryData = GetDictionryFromJson();

            foreach (var item in airports!)
            {
                item.ISO2 = countryData?.FirstOrDefault(x => x.Value == item.country).Key;

            }
            return airports;
        }
        return null;
    }

    public Dictionary<string,string>? GetDictionryFromJson()
    {
        var startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.
            Parent?.FullName!,"CountryCodes.json");

        var data = File.ReadAllText(startupPath);

        var countryData = JsonConvert.DeserializeObject<Dictionary<string,string>>(data);

        return countryData;
    }
}

