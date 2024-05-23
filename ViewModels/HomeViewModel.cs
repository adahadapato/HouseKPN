

namespace HouseKPN.ViewModels;

using HouseKPN.Infrastructures;
using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;

public class HomeViewModel : ViewModel
{
    private INavigationService _navigation;
    private RegistryService _registryService;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged(nameof(Navigation));
        }
    }

    public HomeViewModel(INavigationService navServie,
                         RegistryService registryService)
    {
        _navigation = navServie;
        _registryService = registryService;
    }
    public RelayCommand NavigateToLoginCommand
    {
        get
        {
            return new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);
        }
    }

    public RelayCommand NavigateToDashbordCommand
    {
        get
        {
            return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
        }
    }

    public RelayCommand CloseWindowCommand
    {
        get
        {
            return new RelayCommand(o => { Navigation.NavigateTo<DashbordViewModel>(); }, o => true);
        }

    }

}
