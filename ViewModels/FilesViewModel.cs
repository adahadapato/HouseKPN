using HouseKPN.Dto;
using HouseKPN.Infrastructures;
using HouseKPN.Models;
using HouseKPN.Resources.Interfaces;
using HouseKPN.Resources.Services;
using HouseKPN.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace HouseKPN.ViewModels
{
    class FilesViewModel : ViewModel
    {
        private INavigationService _navigation;
        private ObservableCollection<FilesModel> _files;
        private readonly IResourceService _resourceService;
        private readonly RegistryService _registryService;
        public FilesViewModel(INavigationService navServie, RegistryService registryService, IResourceService resourceService)
        {
            _navigation = navServie;
            _registryService = registryService;
            _resourceService = resourceService;
            LoadBtnText = "Load";
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

        public RelayCommand NavigateToDashbordCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand<object> PushAllCommand
        {
            get
            {
                return new RelayCommand<object>(async (object e) => await PushAllToServerAsync(e));
            }
        }

        public RelayCommand<object> PushToServerCommand
        {
            get
            {
                return new RelayCommand<object>(async (object e) => await PushToServerAsync(e));
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


        private string _loadBtnText;
        public string LoadBtnText
        {
            get => _loadBtnText;
            set
            {
                SetProperty(ref _loadBtnText, value);
                OnPropertyChanged(nameof(LoadBtnText));
            }
        }

        private async void Init()
        {
            //LoadBtnText = "Loading ...";
            Files.Clear();
            var exts = new[] { ".sos", ".def" };


            var localFileList = Directory.GetFiles(_registryService.SosDir, "*.*", SearchOption.AllDirectories)
                    .Where(d => !d.Contains("Images") && !exts.Any(x => d.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToList();

            var _examType = ExamsUtility.GetExamType(_registryService.ExamType);
           
            if (localFileList == null || localFileList.Count == 0) return;
            var (_success, _message, _remoteFiles) = (_examType == "NCEE" || _examType == "GIFT")
                ? await _resourceService.GetInventory(_examType, _registryService.ExamYear, _registryService.DeviceId)
                : await _resourceService.GetInventory(_examType, _registryService.Job, _registryService.ExamYear, _registryService.DeviceId);

            var _missingFiles = localFileList.Where(d => !_remoteFiles.Any(r => r.FileName == Path.GetFileName(d))).ToList();

            Records = _missingFiles.Count;

            if (_missingFiles.Count == 0)
            {
                MessageBox.Show("There are no missing files available to load\n Operation is terminated", "Load missing files", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                

            Records = _missingFiles.Count;

            var message = $"There is/are {_missingFiles.Count} files that are not saved on the server\n"
                + "It is recommended that the file(s) be saved to the server to avoid possible data loss\n"
                + "Please inform the shift suppervisor for necessary action.";
            MessageBox.Show(message, "House keeping", MessageBoxButton.OK, MessageBoxImage.Warning);


            var (status, msg, subjects) = await _resourceService.GetSubjects(_registryService.ExamType, _registryService.Job, _registryService.ExamYear);
            if (subjects.Count == 0) return;
            //var _fileModel = new List<FilesModel>();
            string subj = "", NTSCode, NTSubject;
            foreach (var s in _missingFiles)
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
                //_fileModel.Add(new FilesModel
                //{
                //    FileName = s,
                //    OperatorId = _opId,
                //    Records = _size,
                //    SubjectCode = temp,
                //    Subjectname = subj,
                //});

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

            foreach (var f  in Files)
            {
                f.PropertyChanged += FilesOnPropertyChanged;
            }
        }
        private void FilesOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            // Only re-check if the IsChecked property changed
            if (args.PropertyName == nameof(FilesModel.IsChecked))
                RecheckAllSelected();
        }


        private void AllSelectedChanged()
        {
            if(Files == null) return;
            // Has this change been caused by some other change?
            // return so we don't mess things up
            if (_allSelectedChanging) return;

            try
            {
                _allSelectedChanging = true;

                // this can of course be simplified
                if (AllSelected == true)
                {
                    foreach (FilesModel f in Files)
                        f.IsChecked = true;
                }
                else if (AllSelected == false)
                {
                    foreach (FilesModel f in Files)
                        f.IsChecked = false;
                }
            }
            finally
            {
                _allSelectedChanging = false;
            }
        }

        private void RecheckAllSelected()
        {
            // Has this change been caused by some other change?
            // return so we don't mess things up
            if (_allSelectedChanging) return;

            try
            {
                _allSelectedChanging = true;

                if (Files.All(e => e.IsChecked))
                    AllSelected = true;
                else if (Files.All(e => !e.IsChecked))
                    AllSelected = false;
                else
                    AllSelected = null;
            }
            finally
            {
                _allSelectedChanging = false;
            }
        }

        public bool? AllSelected
        {
            get => _allSelected;
            set
            {
                //if (value == _allSelected) return;
                //_allSelected = value;

                SetProperty(ref _allSelected, value);
                // Set all other CheckBoxes
                AllSelectedChanged();
                OnPropertyChanged(nameof(AllSelected));

                
                //OnPropertyChanged();
            }
        }

        private bool _allSelectedChanging;
        private bool? _allSelected;




        private async Task PushAllToServerAsync(object e)
        {
            try
            {
                if (Records == 0)
                {
                    MessageBox.Show("There are no files available to push to server\n Operation terminated", "Push file to server", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (e == null)
                {
                    //MessageBox.Show("This feature is not avalaible at this time", "Push file to server", MessageBoxButton.OK, MessageBoxImage.Error);

                    MessageBox.Show("No file selected", "Push file to server", MessageBoxButton.OK, MessageBoxImage.Error);
                    await Task.CompletedTask;
                    return;
                }
                //List<FilesModel>? _files_ = e as List<FilesModel>;


                var msg = $"Are you sure you want to save these files ";
                if (MessageBox.Show(msg, "Save to Server", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;

                //var FileName = LblOwner.Text.Trim();

                string ExamYear = _registryService.ExamYear;

                var Job = _registryService.Job;
                var Exams = _registryService.ExamType;
                string scanType;
                var _scanDataRequest = new List<ScanDataRequest>();
                foreach (var _file in Files)
                {

                    if(!_file.IsChecked) continue;

                    var IsDiscard = _file.FileName.Contains("Disc_");
                    var Userid = _file.OperatorId;
                    var _fileToSave = System.IO.Path.GetFileName(_file.FileName);
                    var systemNo = _registryService.DeviceId;
                    var subject = _file.Subjectname;
                    //var paper = NTSubject.Substring(4, 1);
                    string scanDir;
                    if (IsDiscard)
                    {
                        var tempDir = _registryService.SosDir;
                        scanDir = tempDir.Substring(3, (tempDir.Length - 3));
                        scanType = $"Disc_{systemNo}";
                    }
                    else
                    {
                        var tempDir = _registryService.SosDir;
                        scanDir = tempDir.Substring(3, (tempDir.Length - 3));
                        scanType = $"{_registryService.ScanType}_{systemNo}";
                    }

                    var scanData = ScanfileUtility.GetFileData(_file.FileName);
                    var Data = new ScanDataRequest
                    {
                        ScanFile = Path.GetFileName(_file.FileName),
                        JobDir = scanDir,
                        Responses = scanData,
                        Job = Job,
                        ExamType = (Exams == "SSCE") ? $"{Exams} {_registryService.Examination}" : Exams,
                        ScanType = scanType,
                        SystemNo = systemNo,
                        OperatorId = Userid,
                        Subject = (Exams.Contains("NCEE")) ? $"Paper {subject}" : subject,
                        ExamYear = ExamYear
                    };

                    _scanDataRequest.Add(Data);
                }

                var (result, message) = await _resourceService.SaveToServer(ExamsUtility.GetExamType(_registryService.ExamType), _registryService.Job, _scanDataRequest);
                if (!result)
                {
                    if (message.Contains("rename"))
                    {
                        var idx = message.LastIndexOf(':');
                        var newFileName = message.Substring(idx + 1, 11);
                        var _newFileName = newFileName;
                        //BtnRenameFile.Enabled = !string.IsNullOrWhiteSpace(_newFileName);
                        var msg1 = $"{message}\nPlease click rename file button to rename this file";
                        MessageBox.Show(msg1, "House keeping", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show(message, "House keeping", MessageBoxButton.OK);
                        Init();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "House keeping", MessageBoxButton.OK);
            }
        }

        private async Task PushToServerAsync(object e)
        {
            try
            {
                if(Records == 0)
                {
                    MessageBox.Show("There are no files available to push to server\n Operation terminated", "Push file to server", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (e == null)
                {
                    MessageBox.Show("No file selected", "Push file to server", MessageBoxButton.OK, MessageBoxImage.Error);
                    await Task.CompletedTask;
                    return;
                }
                FilesModel? _file = e as FilesModel;


                var msg = $"Are you sure you want to save file {_file.FileName}\n" +
                   $"for this operator {_file.OperatorName} ";
                if (MessageBox.Show(msg, "Save to Server", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;

                //var FileName = LblOwner.Text.Trim();

                string ExamYear = _registryService.ExamYear;
                var IsDiscard = _file.FileName.Contains("Disc_");
                var Job = _registryService.Job;
                var Exams = _registryService.ExamType;
                string scanType;
                var Userid = _file.OperatorId;
                var _fileToSave = System.IO.Path.GetFileName(_file.FileName);
                var systemNo = _registryService.DeviceId;
                var subject = _file.Subjectname;
                //var paper = NTSubject.Substring(4, 1);
                string scanDir;
                if (IsDiscard)
                {
                    var tempDir = _registryService.SosDir;
                    scanDir = tempDir.Substring(3, (tempDir.Length - 3));
                    scanType = $"Disc_{systemNo}";
                }
                else
                {
                    var tempDir = _registryService.SosDir;
                    scanDir = tempDir.Substring(3, (tempDir.Length - 3));
                    scanType = $"{_registryService.ScanType}_{systemNo}";
                }

                var scanData = ScanfileUtility.GetFileData(_file.FileName);
                var Data = new ScanDataRequest
                {
                    ScanFile = Path.GetFileName(_file.FileName),
                    JobDir = scanDir,
                    Responses = scanData,
                    Job = Job,
                    ExamType = (Exams == "SSCE") ? $"{Exams} {_registryService.Examination}" : Exams,
                    ScanType = scanType,
                    SystemNo = systemNo,
                    OperatorId = Userid,
                    Subject = (Exams.Contains("NCEE")) ? $"Paper {subject}" : subject,
                    ExamYear = ExamYear
                };

                var (result, message) = await _resourceService.SaveToServer(ExamsUtility.GetExamType(_registryService.ExamType), _registryService.Job, Data);
                if (!result)
                {
                    if (message.Contains("rename"))
                    {
                        var idx = message.LastIndexOf(':');
                        var newFileName = message.Substring(idx + 1, 11);
                        var _newFileName = newFileName;
                        //BtnRenameFile.Enabled = !string.IsNullOrWhiteSpace(_newFileName);
                        var msg1 = $"{message}\nPlease click rename file button to rename this file";
                        MessageBox.Show(msg1, "House keeping", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show(message, "House keeping", MessageBoxButton.OK);
                        Init();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "House keeping", MessageBoxButton.OK);
            }
        }
    }
}
