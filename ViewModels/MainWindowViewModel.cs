using System;
using System.Collections.ObjectModel;

using PropertyChanged;

using Travel.Models;
using Travel.Services;

using Windows.Devices.Geolocation;

namespace Travel.ViewModels;

[AddINotifyPropertyChangedInterface]
public class MainWindowViewModel
{
    public GeoServices GeoServices
    {
        get; set;
    }
    public AirportServices Services
    {
        get; set;
    }

    public ObservableCollection<Airport> AirportsList
    {
        get; set;
    }

    public Airport? SelectedItem
    {
        get; set;
    }

    public string? currentCity
    {
        get; set;
    }

    public MainWindowViewModel()
    {
        //SelectedItem = new Airport
        //{
        //    OnAnyPropertiesChanged = () => { MyProperty?.RaiseCanExecuteChanged(); }
        //};
        Services = new AirportServices();

        GeoServices = new GeoServices();

        AirportsList = new ObservableCollection<Airport>();

        GetAirportList();

        GetOrigin();
    }
    private async void GetAirportList()
    {
        var temppList = await Services.GetAirportsAsync();

        var teplObservable = new ObservableCollection<Airport>();
        foreach (var item in temppList!)
        {
            teplObservable.Add(item);
        }

        AirportsList = teplObservable;

    }

    private async void GetOrigin()
    {
        var accessStatus = await Geolocator.RequestAccessAsync();

        if (accessStatus == GeolocationAccessStatus.Allowed)
        {
            var locator = new Geolocator();

            var pos = await locator.GetGeopositionAsync();

            var userCity = await GeoServices.GetLocation(pos.Coordinate.Latitude,pos.Coordinate.Longitude);

            currentCity = userCity?.address?.city;

        }
    }
}