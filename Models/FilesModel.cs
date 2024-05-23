using HouseKPN.ViewModels;

namespace HouseKPN.Models;

internal sealed class FilesModel: ViewModel
{
    public string FileName { get; set; }
    public int Records { get; set; }
    public string OperatorId { get; set; }
    public string OperatorName { get; set; }
    public string SubjectCode { get; set; }
    public string Subjectname { get; set; }
    public string DateScanned { get; set; }
    public List<string> Content { get; set; }


    private bool _isChecked;

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (value == _isChecked) return;
            _isChecked = value;
            OnPropertyChanged();
        }
    }
}
