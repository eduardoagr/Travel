using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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

            foreach (var item in airports!)
            {
                var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
                var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(item.country!));
                var countryAbbrev = englishRegion?.TwoLetterISORegionName;
                item.ISO2 = countryAbbrev;
            }
        }
        return airports;
    }


}

