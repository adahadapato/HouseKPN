namespace HouseKPN.Dto
{
    public class LoginResponse
    {
        public string PersonnelNo { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Access_token { get; set; }
    }

    public class LoginRequest
    {
        public string PersonnelNo { get; set; }
        public string Password { get; set; }
    }
}
