using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Travel.Models;

namespace Travel.Services;
public class AirportServices
{
    readonly HttpClient Client;
    public AirportServices()
    {
        Client = new HttpClient();
    }

    List<Airport>? airports = new();
    public async Task<List<Airport>> GetAirportsAsync()
    {
        if (airports?.Count > 0)
        {
            return airports;
        }

        var url = "https://gist.githubusercontent.com/tdreyno/4278655/raw/7b0762c09b519f40397e4c3e100b097d861f5588/airports.json";

        var response = await Client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            airports = await response.Content.ReadFromJsonAsync<List<Airport>>();
        }

        return airports!;
    }

}
