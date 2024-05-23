using System.Net.Http;

namespace HouseKPN.Resources.Services
{
    public sealed class CandidateService
    {
        private readonly HttpClient _httpClient;
        public CandidateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
