

using HouseKPN.Dto;
using HouseKPN.Infrastructures;
using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;
using System.Windows;

namespace HouseKPN.ViewModels;

public class LoginViewModel : ViewModel
{
    private readonly IResourceService _resourceService;
    private INavigationService _navigation;
    private readonly ITokenContainer _tokenContainer;
    //private readonly RegistryService _registryService;


    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged(nameof(Navigation));
        }
    }

    private string _password;
    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public RelayCommand CloseWindowCommand
    {
        get
        {
            return new RelayCommand(o=> { Navigation.NavigateTo<DashbordViewModel>(); }, o=> true);
        }

    }

    public RelayCommand LoginCommand
    {
        get
        {
            return new RelayCommand(async (o) => await Login());
        }

    }
    private readonly MainViewModel _mainViewModel;
    public LoginViewModel(INavigationService navService, 
                         IResourceService resourceService,
                         MainViewModel mainViewModel,
                         ITokenContainer tokenContainer)
    {
        Navigation = navService;
        _resourceService = resourceService;
        _mainViewModel = mainViewModel;
        _tokenContainer = tokenContainer;
        //_registryService = registryService;
    }

    private async Task Login()
    {
       
        if(string.IsNullOrWhiteSpace(UserName))
        {
            MessageBox.Show("Invalid user name", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            MessageBox.Show("Invalid password", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        var _request = new LoginRequest
        {
            PersonnelNo = UserName,
            Password = Password,
        };
        var (_success, _message, _result) = await _resourceService.Login(_request);
        if(!_success)
        {
            MessageBox.Show(_message,"Login", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        _tokenContainer.SetToken(_result.Access_token);
        //var (_status, _msg, _staff) = await _resourceService.GetStaffDetails(UserName);
        //if(!_status)
        //{
        //    MessageBox.Show(_msg,"Get Staff details", MessageBoxButton.OK, MessageBoxImage.Error);
        //    return;
        //}

        var spc = (_result.Name.Count(x => char.IsWhiteSpace(x))) + 1;
        var split = _result.Name.Split([' '], spc);


        var _firstName = split[1];
        var _lastName = split[0];
        var _middleName = ((spc > 2) ? split[2] : "");

        var _shortName = $"{_firstName.Trim()} {_middleName}";

        //_registryService.FullName= _shortName;
        _mainViewModel.FullName = _shortName;
        //_registryService.PersonnelNo = _result.PersonnelNo;
        _mainViewModel.PersonnelNumber = _result.PersonnelNo;
        //_mainViewModel.Picture = _staff.ToBitmap();
        // _registryService.LogOut = false;
        //_mainViewModel.ShowLogIn = _registryService.LogOut;
        //_mainViewModel.ShowLogOut = !_registryService.LogOut;
        Navigation.NavigateTo<DashbordViewModel>();
        
    }
}
