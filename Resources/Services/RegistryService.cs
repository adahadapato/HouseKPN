namespace HouseKPN.Resources.Services;

public class RegistryService : RegistryToken
{
    public RegistryService()
    {
       
    }


    public string ApiToken
    {
        get { return GetValue("ApiToken").ToString(); }
        set { SetValue("ApiToken", value); }
    }
    public int TokenExpiryDate
    {
        get { return Convert.ToInt32(GetValue("TokenExpiryDate")); }
        set { SetValue("TokenExpiryDate", value.ToString()); }
    }

    public int TotalScanned
    {
        get { return Convert.ToInt32(GetValue("TotalScanned")); }
        set { SetValue("TotalScanned", value.ToString()); }
    }

    public string DeviceId
    {
        get { return GetValue("DeviceId").ToString(); }
        set { SetValue("DeviceId", value); }
    }
    //
    public string SosDir
    {
        get { return GetValue("sosDir").ToString(); }
        set { SetValue("sosDir", value); }
    }

    public bool StartBatch
    {
        get { return Convert.ToBoolean(GetValue("startBatch")); }
        set { SetValue("startBatch", value.ToString()); }
    }

    public string InconsistentFiles
    {
        get { return GetValue("InconsistentFile"); }
        set { SetValue("InconsistentFile", value); }
    }

    public int InconsistentFilesCount
    {
        get { return Convert.ToInt32(GetValue("InconsistentFilesCount")); }
        set { SetValue("InconsistentFilesCount", value.ToString()); }
    }

    public string SOSInpBatchFileName
    {
        get { return GetValue("SOSInpBatchFileName").ToString(); }
        set { SetValue("SOSInpBatchFileName", value); }
    }
    public string ScanStatus
    {
        get { return GetValue("Status").ToString(); }
        set { SetValue("Status", value); }
    }
    public string ExamType
    {
        get { return GetValue("examType").ToString(); }
        set { SetValue("examType", value); }
    }

    public string Examination
    {
        get { return GetValue("examination").ToString(); }
        set { SetValue("examination", value); }
    }
    public string ExamYear
    {
        get { return GetValue("examYear").ToString(); }
        set { SetValue("examYear", value); }
    }
    //
    public int ExpectedSheets
    {
        get { return Convert.ToInt32(GetValue("ExpectedSheets").ToString()); }
        set { SetValue("ExpectedSheets", value.ToString()); }
    }
    public string Job
    {
        get { return GetValue("Job").ToString(); }
        set { SetValue("Job", value); }
    }
    public bool LogOut
    {
        get { return Convert.ToBoolean(GetValue("Logout")); }
        set { SetValue("Logout", value.ToString()); }
    }
    public string BatchNumber
    {
        get { return GetValue("batchnumber").ToString(); }
        set { SetValue("batchnumber", value); }
    }
    public string ScanType
    {
        get { return GetValue("ScanType").ToString(); }
        set { SetValue("ScanType", value); }
    }

    public string ScanDir
    {
        get { return GetValue("scanDir").ToString(); }
        set { SetValue("scanDir", value); }
    }
    public string Status
    {
        get { return GetValue("Status").ToString(); }
        set { SetValue("Status", value); }
    }
    //
    public bool IsBlind
    {
        get { return Convert.ToBoolean(GetValue("Blind")); }
        set
        {
            SetValue("Blind", value.ToString());
        }
    }
    public bool IsSuplementary
    {
        get { return Convert.ToBoolean(GetValue("Suplementary")); }
        set
        {
            SetValue("Suplementary", value.ToString());
        }
    }
    public bool IsResit
    {
        get { return Convert.ToBoolean(GetValue("resit")); }
        set
        {
            SetValue("resit", value.ToString());
        }
    }

    public bool IsBlank
    {
        get { return Convert.ToBoolean(GetValue("Blank")); }
        set
        {
            SetValue("Blank", value.ToString());
        }
    }
    public bool IsAdmin
    {
        get { return Convert.ToBoolean(GetValue("IsAdmin")); }
        set
        {
            SetValue("IsAdmin", value.ToString());
        }
    }

    public string UID
    {
        get { return GetValue("UID").ToString(); }
        set
        {
            SetValue("UID", value);
        }
    }
    //Blank
    public string ShortSubj
    {
        get { return GetValue("shortsubj").ToString(); }
        set { SetValue("shortsubj", value); }
    }
    public string Subject
    {
        get { return GetValue("subject").ToString(); }
        set { SetValue("subject", value); }
    }

    public string SubjCode
    {
        get { return GetValue("SubjCode").ToString(); }
        set { SetValue("SubjCode", value); }
    }
    public string State
    {
        get { return GetValue("state").ToString(); }
        set { SetValue("state", value); }
    }

    public string StateCode
    {
        get { return GetValue("stateCode").ToString(); }
        set { SetValue("stateCode", value); }
    }
    public string OperatorId
    {
        get { return GetValue("OperatorId").ToString(); }
        set { SetValue("OperatorId", value); }
    }

    public int AnswerSheet
    {
        get { return Convert.ToInt32(GetValue("AnswerSheet").ToString()); }
        set { SetValue("AnswerSheet", value.ToString()); }
    }
    //paper
    public string Paper
    {
        get { return GetValue("paper").ToString(); }
        set { SetValue("paper", value); }
    }
    public string LastDecodeLoaded
    {
        get { return GetValueEx("LastDecodeLoaded").ToString(); }
        set { SetValueEx("LastDecodeLoaded", value); }
    }

    public bool FirstStart
    {
        get { return Convert.ToBoolean(GetValue("first")); }
        set { SetValue("first", value.ToString()); }
    }

    //public bool LogOut
    //{
    //    get { return Convert.ToBoolean(GetValue("Logout")); }
    //    set { SetValue("Logout", value.ToString()); }
    //}

    //public string ApiToken
    //{
    //    get { return GetValue("ApiToken").ToString(); }
    //    set { SetValue("ApiToken", value); }
    //}
    //public string ExamType
    //{
    //    get { return GetValue("examType")?.ToString(); }
    //    set { SetValue("examType", value); }
    //}

    //public string Examination
    //{
    //    get { return GetValue("examination")?.ToString(); }
    //    set { SetValue("examination", value); }
    //}
    //public string ExamYear
    //{
    //    get { return GetValue("examYear")?.ToString(); }
    //    set { SetValue("examYear", value); }
    //}
    public string ExamsDetails
    {
        get { return GetValue("examsDetails")?.ToString(); }
        set { SetValue("examsDetails", value); }
    }

    public string FullName
    {
        get => GetValue("fullName")?.ToString();
        set => SetValue("fullName", value);
    }

    public string PersonnelNo
    {
        get { return GetValue("personnelNo")?.ToString(); }
        set { SetValue("personnelNo", value); }
    }

    public override string BaseKey()
    {
        throw new NotImplementedException();
    }
}
