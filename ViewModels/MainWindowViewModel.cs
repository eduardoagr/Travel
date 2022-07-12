using System.Collections.Generic;
using System.Windows;

using MvvmHelpers;
using MvvmHelpers.Commands;

using PropertyChanged;

using Travel.Models;
using Travel.Services;

namespace Travel.ViewModels;

[AddINotifyPropertyChangedInterface]
public class MainWindowViewModel 
{
    public Command pp
    {
        get; set;
    }
    public AirportServices Services
    {
        get; set;
    }

    public List<Airport> AirportsList
    {
        get; set;
    }

    public Airport? airport
    {
        get; set;
    }

    public MainWindowViewModel()
    {
        Services = new AirportServices();

        AirportsList = new List<Airport>();

        GetAirportList();

        airport = new Airport();

        pp = new Command(DoSomething,CanDoIT);
    }

    private bool CanDoIT(object arg)
    {
        if (!string.IsNullOrEmpty(airport.births))
        {
            return true;
        }

        return false;
    }
    private void DoSomething(object obj)
    {
        MessageBox.Show("dwdew");
    }

    private async void GetAirportList()
    {
        var tempList = await Services.GetAirportsAsync();

        foreach (var item in tempList)
        {
            AirportsList.Add(item);
        }
    }
}
