
using System.Windows;

using PropertyChanged;

using Travel.Models;

namespace Travel.ViewModels;

[AddINotifyPropertyChangedInterface]
public class CityDetailsViewModel
{
    public Airport? SelectedItem
    {
        get; set;
    }
    public CityDetailsViewModel()
    {

    }

    public CityDetailsViewModel(Airport airport)
    {
        SelectedItem = airport;

        MessageBox.Show($"{SelectedItem.city}");
    }
}
