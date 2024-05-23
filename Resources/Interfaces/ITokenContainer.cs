namespace HouseKPN.Resources.Interfaces
{
    public interface ITokenContainer
    {
        string GetToken();
        void SetToken(string token);
        string RefreshToken();
    }
}
