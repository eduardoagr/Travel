using System.Collections.ObjectModel;
using System.Windows;

using MvvmHelpers;
using MvvmHelpers.Commands;

using PropertyChanged;

using Travel.Models;
using Travel.Services;

namespace Travel.ViewModels;

[AddINotifyPropertyChangedInterface]
public class MainWindowViewModel : BaseViewModel
{
    public Command MyProperty
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

    public MainWindowViewModel()
    {
        SelectedItem = new Airport
        {
            OnAnyPropertiesChanged = () => { MyProperty?.RaiseCanExecuteChanged(); }
        };
        Services = new AirportServices();

        AirportsList = new ObservableCollection<Airport>();

        MyProperty = new Command(Do,CanDo);

        GetAirportList();
    }

    private bool CanDo(object arg)
    {
        return SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.name);
    }
    private void Do(object obj)
    {
        MessageBox.Show("sdc");
    }

    private async void GetAirportList()
    {
        var temppList = await Services.GetAirportsAsync("https://gist.githubusercontent.com/tdreyno/4278655/raw/7b0762c09b519f40397e4c3e100b097d861f5588/airports.json");

        var airports = new ObservableCollection<Airport>();
        foreach (var item in temppList!)
        {
            airports.Add(item);
        }

        AirportsList = airports;

    }
}