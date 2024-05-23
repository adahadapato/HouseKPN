using HouseKPN.Infrastructures;
using HouseKPN.Models;
using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;
using HouseKPN.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace HouseKPN.ViewModels
{
    class EventsViewModel : ViewModel
    {
        private INavigationService _navigation;
        private ObservableCollection<FilesModel> _files;
        private readonly IResourceService _resourceService;
        private readonly RegistryService _registryService;
        public EventsViewModel(INavigationService navServie, 
            RegistryService registryService, 
            IResourceService resourceService)
        {
            _navigation = navServie;
            _registryService = registryService;
            _resourceService = resourceService;
            
            
        }

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        private int _records;
        public int Records
        {
            get => _records;
            set
            {
                SetProperty(ref _records, value);
                ShowRecords = $"No. of files: {Records}";
                OnPropertyChanged(nameof(Records));
            }
        }


        private string _showRecords;
        public string ShowRecords
        {
            get => _showRecords;
            set
            {
                SetProperty(ref _showRecords, value);
                OnPropertyChanged(nameof(ShowRecords));

            }
        }

        public RelayCommand DashbordCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand LoadCommand
        {
            get
            {
                return new RelayCommand(o => Init());
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                return new RelayCommand(o => Init());
            }
        }


        public ObservableCollection<FilesModel> Files
        {
            get => _files ??= new ObservableCollection<FilesModel>();
            set
            {
                SetProperty(ref _files, value);
                OnPropertyChanged(nameof(Files));
            }
        }

        private async Task Init()
        {
            Files.Clear();
            var exts = new[] { ".sos", ".def" };

            var localFileList = Directory.GetFiles(_registryService.SosDir, "*.*", SearchOption.AllDirectories)
                    .Where(d => !d.Contains("Images") && !exts.Any(x => d.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToList();

            var _examType = ExamsUtility.GetExamType(_registryService.ExamType);

            if (localFileList == null || localFileList.Count == 0) return;

            var (status, msg, subjects) = await _resourceService.GetSubjects(_registryService.ExamType, _registryService.Job, _registryService.ExamYear);
            if (subjects.Count == 0) return;
            Records = localFileList.Count;
            string subj = "", NTSCode, NTSubject;

            foreach (var s in localFileList)
            {
                var _size = ScanfileUtility.GetScanFileSize(s);
                var _owner = ScanfileUtility.GetScanFileOwnerName(s, _registryService.Job);
                var _opId = ScanfileUtility.GetScanFileOwnerId(s);
                var _date = ScanfileUtility.GetScanFileCreationDate(s);
                var temp = ScanfileUtility.GetScanFileSubject(s, _registryService.Job, _registryService.ExamType);
                if (temp == null || temp.Length < 4) continue;


                if (_registryService.Job == "Obj")
                {
                    NTSCode = temp;
                    subj = (_registryService.ExamType == "BECE") ? subjects.Where(x => x.Code.Substring(1, 2) == temp.Substring(1, 2)).Select(x => x.Subject).FirstOrDefault() :
                         subjects.Where(x => x.Code == temp).Select(x => x.Subject).FirstOrDefault(); ;
                    NTSubject = subj;
                }


                if (_registryService.Job == "Essay" && _registryService.ExamType != "BECE")
                {
                    NTSCode = temp;
                    subj = subjects.Where(x => x.Code.Substring(0, 3) == temp.Substring(0, 3)).Select(x => x.Subject).FirstOrDefault();
                    NTSubject = subj;
                }

                if (_registryService.Job == "Essay" && _registryService.ExamType == "BECE")
                {
                    NTSCode = temp;
                    subj = subjects.Where(x => x.Code == temp).Select(x => x.Subject).FirstOrDefault();
                    NTSubject = subj;
                }

                Files.Add(new FilesModel
                {
                    FileName = s,
                    OperatorId = _opId,
                    Records = _size,
                    SubjectCode = temp,
                    Subjectname = subj,
                    DateScanned = _date,
                    OperatorName = _owner
                });
            }

            foreach (var f in Files)
            {
                f.PropertyChanged += FilesOnPropertyChanged;
            }
        }

        private void FilesOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            // Only re-check if the IsChecked property changed
            //if (args.PropertyName == nameof(FilesModel.IsChecked)) 
               // RecheckAllSelected();
        }
    }
}
