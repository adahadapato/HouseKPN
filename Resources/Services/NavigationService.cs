using HouseKPN.Resources.Interfaces;
using HouseKPN.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HouseKPN.Resources.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private ViewModel _currentView;

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public void NavigateTo<T>() 
            where T : ViewModel
        {
            ViewModel _viewModel = _viewModelFactory.Invoke(typeof(T));
            CurrentView = _viewModel;
        }
    }
}
