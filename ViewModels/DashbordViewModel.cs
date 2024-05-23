using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;

namespace HouseKPN.ViewModels
{
    public class DashbordViewModel : ViewModel
    {
        private INavigationService _navigation;
        private readonly RegistryService _registryService;
        #region class properties
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                SetProperty(ref _navigation, value);
                OnPropertyChanged(nameof(Navigation));
            }
        }
        #endregion

        #region Class cunstructor
        public DashbordViewModel(INavigationService navService,
                                 RegistryService registryService)
        {
            Navigation = navService;
            _registryService = registryService;
        }
        #endregion
    }
}
