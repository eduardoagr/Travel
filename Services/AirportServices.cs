using System;
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

            Dictionary<string, HttpResponseMessage> countryInfoCache = new Dictionary<string, HttpResponseMessage>();

            int requestCount = 0;

            for (var i = 0; i < airports?.Count; i++)
            {
                using var client = new HttpClient();

                if (airports[i].country == null) continue;

                try
                {
                    HttpResponseMessage result;
                    if (countryInfoCache.ContainsKey(airports[i].country))
                    {
                        result = countryInfoCache[airports[i].country];
                    }
                    else
                    {
                        result = await client.GetAsync
                            ($"https://countrycode.dev/api/countries/{airports[i]
                            .country?.Replace(" ", "%20")}");

                        countryInfoCache[airports[i].country] = result;

                        requestCount++;
                    }

                    if (requestCount >= 40)
                    {
                        Console.Write("Reached 40, waiting for 1 second");
                        await Task.Delay(1000);
                        requestCount = 0;
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();

                        var countriesArray = JArray.Parse(response);

                        if (countriesArray.Count > 0 && countriesArray[0]["country_name"]?.Value<string>() == airports[i].country)
                        {
                            airports[i].ISO2 = countriesArray[0]["ISO2"]?.ToString();
                        }
                    }
                    else
                    {
                        Console.Write("Error: " + result.ToString());
                    }
                }
                catch(Exception e)
                {
                    Console.Write("Exception: " + e.ToString());
                }

            }
            return airports;
        }
        return null;
    }
}

