using System.Collections.ObjectModel;
using System.Windows;

using MvvmHelpers.Commands;

using PropertyChanged;

using Travel.Models;
using Travel.Services;

namespace Travel.ViewModels;

[AddINotifyPropertyChangedInterface]
public class MainWindowViewModel
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

    public string? text
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
        if (SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.name))
        {
            text = "Enabled";

            return true;
        }

        text = "Disable";
        return false;
    }
    private void Do(object obj)
    {
        MessageBox.Show("sdc");
    }

    private async void GetAirportList()
    {
        Services.GetAirportsAsync(); // I do not see any data

    }

}