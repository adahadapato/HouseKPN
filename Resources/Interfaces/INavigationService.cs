using HouseKPN.ViewModels;

namespace HouseKPN.Resources.Interfaces
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
