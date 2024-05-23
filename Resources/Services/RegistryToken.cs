using Microsoft.Win32;
using System.Windows;

namespace HouseKPN.Resources.Services;

//public interface IRegistryService
//{

//}

public abstract class RegistryToken
{
    public virtual string GetValue(string key)
    {
        try
        {
            RegistryKey mICParam = Registry.CurrentUser;
            var mICParams = mICParam.OpenSubKey(@"software\necoscan", true);
            if (mICParams == null)
            {
                mICParam.CreateSubKey($"{mICParams}", true);
                //SetValue("first","true");

            }
            return mICParams?.GetValue(key)?.ToString();

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        return string.Empty;
    }
    public virtual void SetValue(string key, string value)
    {
        try
        {
            RegistryKey mICParam = Registry.CurrentUser;
            var mICParams = mICParam.OpenSubKey(@"software\necoscan", true);
            if (mICParams == null)
            {
                mICParam.CreateSubKey($"{mICParams}", true);
            }
            mICParams?.SetValue(key, value);
            mICParams?.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public virtual string GetValueEx(string regKey)
    {
        try
        {
            RegistryKey mICParams = Registry.CurrentUser;
            mICParams = mICParams.OpenSubKey(@"software\DRS\Sosinpw\Job", true);
            return mICParams.GetValue(regKey).ToString();

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        return string.Empty;
    }


    public virtual void SetValueEx(string regKey, string rValue)
    {
        try
        {
            RegistryKey mICParams = Registry.CurrentUser;
            mICParams = mICParams.OpenSubKey(@"software\DRS\Sosinpw\Job", true);
            mICParams.SetValue(regKey, rValue);
            mICParams.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public abstract string BaseKey();
}
