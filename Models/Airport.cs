using PropertyChanged;

namespace Travel.Models;

[AddINotifyPropertyChangedInterface]
public class Airport
{
    public string? code
    {
        get; set;
    }
    public string? lat
    {
        get; set;
    }
    public string? lon
    {
        get; set;
    }
    public string? name
    {
        get; set;
    }
    public string? city
    {
        get; set;
    }
    public string? state
    {
        get; set;
    }
    public string? country
    {
        get; set;
    }
    public string? woeid
    {
        get; set;
    }
    public string? tz
    {
        get; set;
    }
    public string? phone
    {
        get; set;
    }
    public string? type
    {
        get; set;
    }
    public string? email
    {
        get; set;
    }
    public string? url
    {
        get; set;
    }
    public string? runway_length
    {
        get; set;
    }
    public string? elev
    {
        get; set;
    }
    public string? icao
    {
        get; set;
    }
    public string? direct_flights
    {
        get; set;
    }
    public string? carriers
    {
        get; set;
    }

    private string? _flag;

    public string flag
    {
        get => $"https://countryflagsapi.com/png/{country?.Replace(" ","%20")}";
        set => _flag = value;
    }

    public string births
    {
        get; set;
    }

}