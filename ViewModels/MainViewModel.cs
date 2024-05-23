using HouseKPN.Infrastructures;
using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;
using System.Windows;
using System.Windows.Media;
using System.Xaml;

namespace HouseKPN.ViewModels
{
    public class MainViewModel : ViewModel
    {
       
        private readonly RegistryService _registryService;
        private readonly ITokenContainer _tokenContainer;

        #region class properties
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                SetProperty(ref _navigation, value);
                OnPropertyChanged(nameof(Navigation));
            }
        }

        private bool _isFocused;
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                SetProperty(ref _isFocused, value);
                OnPropertyChanged(nameof(IsFocused));
            }
        }
       
        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                SetProperty(ref _fullName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _personnelNumber = string.Empty;
        public string PersonnelNumber
        {
            get => _personnelNumber;
            set
            {
                SetProperty(ref _personnelNumber, value);
                OnPropertyChanged(nameof(PersonnelNumber));
            }
        }

        private string _operations = string.Empty;
        public string Operations
        {
            get => _operations;
            set
            {
                SetProperty(ref _operations, value);
                OnPropertyChanged(nameof(Operations));
            }
        }

        private string _year = string.Empty;
        public string Year
        {
            get => _year;
            set
            {
                SetProperty(ref _year, value);
                OnPropertyChanged(nameof(Year));
               // _registryService.ExamYear = _year;
            }
        }


        private string _examsDetails = string.Empty;
        public string ExamsDetails
        {
            get => _examsDetails;
            set
            {
                SetProperty(ref _examsDetails, value);
                OnPropertyChanged(nameof(ExamsDetails));
                //App.ExamYear = _year;
            }
        }


        private string _deviceDetails = string.Empty;
        public string DeviceDetails
        {
            get => _deviceDetails;
            set
            {
                SetProperty(ref _deviceDetails, value);
                OnPropertyChanged(nameof(DeviceDetails));
                //App.ExamYear = _year;
            }
        }

        private string _statusDetails = string.Empty;
        public string StatusDetails
        {
            get => _statusDetails;
            set
            {
                SetProperty(ref _statusDetails, value);
                OnPropertyChanged(nameof(StatusDetails));
                //App.ExamYear = _year;
            }
        }

        private string _examination = string.Empty;
        public string Examination
        {
            get => _examination;
            set
            {
                SetProperty(ref _examination, value);
                OnPropertyChanged(nameof(Examination));
               // _registryService.Examination = _examination;
            }
        }


        private string _examsType = string.Empty;
        public string ExamsType
        {
            get => _examsType;
            set
            {
                SetProperty(ref _examsType, value);
                OnPropertyChanged(nameof(ExamsType));
               // _registryService.Examination = _examsType;
            }
        }

        private bool _showLogin;
        public bool ShowLogIn
        {
            get => _showLogin;
            set
            {
                SetProperty(ref _showLogin, value);
                OnPropertyChanged(nameof(ShowLogIn));

            }
        }

        private bool _showLogOut;
        public bool ShowLogOut
        {
            get => _showLogOut;
            set
            {
                SetProperty(ref _showLogOut, value);
                OnPropertyChanged(nameof(ShowLogOut));

            }
        }

        private ImageSource _Picture;
        public ImageSource? Picture
        {
            get => _Picture;
            set
            {
                SetProperty(ref _Picture, value);
                OnPropertyChanged(nameof(Picture));
            }
        }
        #endregion

        #region command objects

        public RelayCommand NavigateToHomeCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<HomeViewModel>(); }, canExecute: o => true);
            }
        }


        public RelayCommand NavigateToDashborCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand NavigateToEventsCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<EventsViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);
            }
        }


        public RelayCommand NavigateToFilesListCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<FilesViewModel>(); }, canExecute: o => true);
            }
        }
        public RelayCommand CloseApplicationCommand
        {
            get
            {
                return new RelayCommand(async o => await SutdownAsync());
            }

        }

        public RelayCommand LogoutCommand
        {
            get
            {
                return new RelayCommand(o => LogOut());
            }

        }

       
        #endregion

        #region class constructor
        public MainViewModel(INavigationService navService,
                            RegistryService registryService, ITokenContainer tokenContainer)
        {
             Navigation = navService;
            _registryService = registryService;
            _tokenContainer = tokenContainer;
            Initialize();
            //Reset();

        }
        #endregion

        #region helper methods
        public Task SutdownAsync()
        {
            Application.Current.Shutdown();
            return Task.CompletedTask;
        }

  
        //public Task SetExamsTypeAsync(object e)
        //{
        //    if(e is null)
        //    {
        //        MessageBox.Show("Invalid exams type");
        //        return Task.CompletedTask;
        //    }
        //    if (string.IsNullOrWhiteSpace(_registryService.ExamYear) || _registryService.ExamYear.Length < 4)
        //    {
        //        MessageBox.Show("Please provide the examination year");
        //        return Task.CompletedTask;
        //    }

        //    var _today = DateTime.Today.Year;
        //    if (Convert.ToInt32(_registryService.ExamYear) > _today)
        //    {
        //        MessageBox.Show("The examination year provided is greater the current year");
        //        return Task.CompletedTask;
        //    }

            
        //    if (string.IsNullOrWhiteSpace(_registryService.ExamYear) || _registryService.ExamYear.Length < 4)
        //    {
        //        MessageBox.Show("Examination year must ne 4 digit long: 2020");
        //        return Task.CompletedTask;
        //    }

        //    Examination = e as string;
        //    GetExamType(e);

        //    if (Convert.ToInt32(_registryService.ExamYear) < 2002  && _registryService.ExamType.ToLower().Contains("ext"))
        //    {
        //        MessageBox.Show("The examination year provided is not valid for SSCE External");
        //        return Task.CompletedTask;
        //    }

        //    GetExamDetails(e);
        //    return Task.CompletedTask;
        //}

        //private void GetExamType(object e)
        //{
        //    var exam = e as string;
        //    if (exam == null) 
        //    {
        //        _registryService.ExamType = string.Empty;
        //    }

        //    _registryService.ExamType =  exam.Trim();

        //    if (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext"))
        //    {
        //        var _exam = exam.AsSpan()[..3];
        //        _registryService.ExamType = _exam.ToString();
        //    }
        //}

        //private void GetExamDetails(object e)
        //{
        //    var exam = e as string;
        //    if (exam == null)
        //    {
        //        _registryService.ExamsDetails = string.Empty;
        //    }
        //    if (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext"))
        //    {
        //      _registryService.ExamsDetails = $"SSCE{_registryService.ExamType}{_registryService.ExamYear}";
        //    }
        //    else
        //    {
        //        _registryService.ExamsDetails = $"{_registryService.ExamType}{_registryService.ExamYear}";
        //    }

        //    ExamsDetails = _registryService.ExamsDetails;
        //}

        private void LogOut()
        {
            if(MessageBox.Show("Do you want to log out now","Logout",MessageBoxButton.YesNo, MessageBoxImage.Question) is MessageBoxResult.No)
            {
                return;
            }

            FullName = "";
            _tokenContainer.SetToken("");
            Picture = null;
            Navigation.NavigateTo<LoginViewModel>();
        }

        

        private void Initialize()
        {
            DeviceDetails = $"System {_registryService.DeviceId}";
            Year = _registryService.ExamYear;
            Examination = _registryService.Examination;
            ExamsType = _registryService.ExamType;
            ExamsDetails = (ExamsType == "BECE" || ExamsType == "NCEE" || ExamsType == "Gifted") ? $"{Year} {ExamsType} Scanning Exercise"
                : $"{Year} {ExamsType} {Examination} Scanning Exercise";
            StatusDetails = ExamsDetails;
            Navigation.NavigateTo<DashbordViewModel>();
        }
        #endregion
    }
}
