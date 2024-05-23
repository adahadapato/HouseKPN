namespace HouseKPN.Dto
{
    public class ScanDataRequest
    {
        public string ExamYear { get; set; }
        public string OperatorId { get; set; }
        public string ScanType { get; set; }
        public string ScanFile { get; set; }
        public string JobDir { get; set; }
        public string ExamType { get; set; }
        public string Job { get; set; }
        public string SystemNo { get; set; }
        public string Subject { get; set; }
        public List<string> Responses { get; set; } = new List<string>();
    }
}
