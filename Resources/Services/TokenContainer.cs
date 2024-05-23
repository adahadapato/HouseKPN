using HouseKPN.Resources.Interfaces;
using System.Windows;

namespace HouseKPN.Resources.Services
{
    public class TokenContainer : ITokenContainer
    {
        //private readonly RegistryService _registryService;
        public TokenContainer()
        {

            //_registryService = registryService;

        }
        public string GetToken()
        {
            try
            {
                return App.access_token;

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Get token",MessageBoxButton.OK,MessageBoxImage.Error);
                return string.Empty;
            }
        }

        public string RefreshToken()
        {
            throw new NotImplementedException();
        }

        public void SetToken(string token)
        {
            try
            {
                App.access_token = token;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Set token", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
