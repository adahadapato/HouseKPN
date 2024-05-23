namespace HouseKPN.Utilities
{
    public  class ExamsUtility
    {
        public static string GetExamType(string examType)
        {
            

            if (examType == "Internal")
            {
                return "SSCE";
            }
            if (examType == "External")
            {
                return "NOV";
            }

            if (examType == "GIFTED")
            {
                return "GIFT";
            }
            return examType;
        }
    }
}
