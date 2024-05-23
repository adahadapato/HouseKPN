using System.IO;
using System.Windows;

namespace HouseKPN.Utilities
{
    public sealed class ScanfileUtility
    {
        public static string GetScanFileSubject(string file, string job, string examType)
        {
            try
            {
                //string input = "";
                var line1 = File.ReadLines(file).FirstOrDefault(); // gets the first line from file.

                int _firstIndex = line1.IndexOf(Path.GetFileName(file));
                //int _lastIndex = line1.IndexOf(Path.GetFileName(file));

                
                _firstIndex -= 5;
                var tempCode = line1.Substring(_firstIndex, line1.Length - _firstIndex)[..4].Where(x => char.IsNumber(x)).ToArray();
                var chCode = new string(tempCode);
                //var _code = chCode.ToString();
                //MessageBox.Show(chCode);
                //if (job == "Obj")
                //{

                //    input = "";//.Substring(122, line1.Length - 122).Trim();
                //}
                //if (job == "Essay")
                //{
                //    if (examType == "SSCE" || examType=="NOV")
                //    {
                //        input = line1.Substring(6, 4).Trim();
                //    }
                //    else
                //    {
                //        input = line1.Substring(62, line1.Length - 62).Trim();
                //    }
                //}

                //string str = input.Substring(0, 4);
                //var s = str.TrimStart('.');
                return chCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get file subject");
            }
            return "";
        }

        public static int GetScanFileSize(string file)
        {
            try
            {
                return File.ReadAllLines(file).Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get file size");
            }
            return 0;
        }

        public static string GetScanFileOwnerName(string file, string job)
        {
            try
            {
                var line1 = File.ReadLines(file).FirstOrDefault(); // gets the first line from file.

                int _firstIndex = line1.IndexOf(Path.GetFileName(file));
                
                var tempCode = line1.Substring(_firstIndex, line1.Length - _firstIndex).Where(x => !char.IsNumber(x)).ToArray();
                var chCode = new string(tempCode).TrimStart('.').TrimEnd(':').Trim();


               // var input = (job == "Obj") ? line1.Substring(122, line1.Length - 122).Trim() : line1.Substring(73, line1.Length - 73);
                //string str = new string(input.Where(c => c != '-' && (c < '0' || c > '9')).ToArray()).Trim();
                //var s = str.TrimStart('.');
                return chCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get file size");
            }
            return "";
        }

        public static string GetScanFileOwnerId(string file)
        {
            try
            {
                var line1 = File.ReadLines(file).FirstOrDefault(); // gets the first line from file.
                var idx = line1.LastIndexOf(':');
                var input = line1.Substring(idx + 1, 4).Trim();
                //string str = new string(input.Where(c => c != '-' && (c < '0' || c > '9')).ToArray()).Trim();
                var s = input;
                return s.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get file size");
            }
            return "";
        }

        public static string GetScanFileCreationDate(string file)
        {
            try
            {
                var date = File.GetCreationTime(file);
                return date.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get file date");
            }
            return "";
        }

        public static List<string> GetFileData(string FileName)
        {
            var lines = File.ReadAllLines(FileName).ToList();
            if (lines.Count == 0)
                return null;
            var scanData = new List<string>();
            foreach (var l in lines)
                scanData.Add(l);

            return scanData;
        }
    }
}
