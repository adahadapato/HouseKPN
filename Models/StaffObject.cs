
using System.IO;
using System.Windows.Media.Imaging;

namespace HouseKPN.Models;

public class StaffObject
{
    public string PersonnelNo { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Division { get; set; }
    public string Passport { get; set; }
    public string FullName()
    {
        return $"{LastName.Trim()} {FirstName.Trim()}";
    }

    public string ShortName()
    {
        return $"{LastName.Trim()} {FirstName.AsSpan()[0..1]}{MiddleName.AsSpan()[0..1]}";
    }
    public BitmapImage? ToBitmap()
    {
        byte[] byteBuffer = Convert.FromBase64String(Passport);
        BitmapImage bitmapImage = new BitmapImage();
        MemoryStream memoryStream = new(byteBuffer)
        {
            Position = 0
        };
       
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memoryStream;
        bitmapImage.EndInit();

        return bitmapImage ?? null;
    }

}
