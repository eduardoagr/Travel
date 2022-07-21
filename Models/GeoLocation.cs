namespace Travel.Models;
public class Address
{
    public string? building
    {
        get; set;
    }
    public string? road
    {
        get; set;
    }
    public string? neighbourhood
    {
        get; set;
    }
    public string? quarter
    {
        get; set;
    }
    public string? suburb
    {
        get; set;
    }
    public string? city
    {
        get; set;
    }
    public string? county
    {
        get; set;
    }
    public string? state
    {
        get; set;
    }
    public string? postcode
    {
        get; set;
    }
    public string? country
    {
        get; set;
    }
    public string? countryCode
    {
        get; set;
    }
    public string flag => $"https://countryflagsapi.com/png/{countryCode}";
}

public class CurrentCity
{
    public Address? address
    {
        get; set;
    }
}
