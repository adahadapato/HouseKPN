using HouseKPN.Dto;
using HouseKPN.Models;
using HouseKPN.Resources.Interfaces;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;

namespace HouseKPN.Resources.Services
{
    public class DataService: IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenContainer _tokenContainer;
        private readonly RegistryService _registryService;
        public DataService(HttpClient httpClient, 
                            ITokenContainer tokenContainer,
                            RegistryService registryService)
        {
            _httpClient = httpClient;
            _tokenContainer = tokenContainer;
            _registryService = registryService;

        }

        /// <summary>
        /// gets all corrections from data base
        /// </summary>
        /// <param name="_staffNo"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, StaffObject? Data)> GetCorrections()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());
                _httpClient.DefaultRequestHeaders.Add("ExaminationId", _registryService.ExamsDetails);
                var response = await _httpClient.GetAsync($"staff/data/number/");
                if (!response.IsSuccessStatusCode) return (false, response.StatusCode.ToString(), null);
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "", null);

                var _response = JsonConvert.DeserializeObject<ApiResponse>(result);

                if (_response == null) return (false, "", null);
                if (!_response.IsSuccessful) return (false, "", null);

                IList collection = (IList)_response.Result;

                var _rec = JsonConvert.DeserializeObject<StaffObject>(JsonConvert.SerializeObject(collection));

                return (true, "", _rec);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}
