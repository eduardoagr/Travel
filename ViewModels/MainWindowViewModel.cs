using System;
using System.Collections.ObjectModel;

using PropertyChanged;

using Travel.Models;
using Travel.Services;
using Travel.Views;

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


    private Airport? _SelectedItem;
    public Airport? SelectedItem
    {
        get => _SelectedItem;
        set
        {
            if (_SelectedItem != value)
            {
                _SelectedItem = value;
                Details();
            }
        }
    }

    public string? CurrentCity
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

        var tempObservable = new ObservableCollection<Airport>();
        foreach (var item in temppList!)
        {
            if (!tempObservable.Contains(item))
            {
                tempObservable.Add(item);
            }

        }

        AirportsList = tempObservable;

    }

    private async void GetOrigin()
    {
        var accessStatus = await Geolocator.RequestAccessAsync();

        if (accessStatus == GeolocationAccessStatus.Allowed)
        {
            var locator = new Geolocator();

            var pos = await locator.GetGeopositionAsync();

            var userCity = await GeoServices.GetLocation(pos.Coordinate.Latitude,pos.Coordinate.Longitude);

            CurrentCity = userCity?.address?.city;
        }
    }

    private void Details()
    {
        if (string.IsNullOrEmpty(SelectedItem?.city)) { return; }

        var CityDetailsWindow = new CittyDetailsWinow();
        new CityDetailsViewModel(SelectedItem);

        CityDetailsWindow.Show();

    }
}