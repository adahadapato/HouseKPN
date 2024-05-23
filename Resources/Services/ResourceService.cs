using HouseKPN.Dto;
using HouseKPN.Models;
using HouseKPN.Resources.Interfaces;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace HouseKPN.Resources.Services
{
    public class ResourceService : IResourceService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenContainer _tokenContainer;
        public ResourceService(HttpClient httpClient, ITokenContainer tokenContainer)
        {
            _httpClient = httpClient;
            _tokenContainer = tokenContainer;

        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, LoginResponse? Data)> Login(LoginRequest data)
        {
            try
            {
                string strPayload = JsonConvert.SerializeObject(data);
                HttpContent _content = new StringContent(strPayload, Encoding.UTF8, "application/json");

               
                var response = await _httpClient.PostAsync("accounts/login", _content);
                if (!response.IsSuccessStatusCode) return (false, response.StatusCode.ToString(), null);
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "An error uccored trying to log in", null);

                var _response = JsonConvert.DeserializeObject<LoginResponse>(result);

                if (_response == null) return (false, "Unable to login at this time", null);
                //if (!_response.IsSuccessful) return (false, _response.ErrorMessages.FirstOrDefault(), null);

                //IList collection = (IList)_response.Result;

                //   var _rec = JsonConvert.DeserializeObject<LoginResponse>(JsonConvert.SerializeObject(collection));

                return (true, "", _response);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new LoginResponse());
            }
        }

        /// <summary>
        /// Gets staff details
        /// </summary>
        /// <param name="_staffNo"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, StaffObject? Data)> GetStaffDetails(string _staffNo)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());
                var response = await _httpClient.GetAsync($"staff/data/number/{_staffNo}");
                if (!response.IsSuccessStatusCode) return (false, response.StatusCode.ToString(),null);
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

        /// <summary>
        /// Gets inventory from server
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="job"></param>
        /// <param name="year"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, List<InventoryResponse> Data)> GetInventory(string exam, string job, string year, string system)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization =
                   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());
                var response = await _httpClient.GetAsync($"inventory/{exam}/{job}/{year}/{system}");
                if (!response.IsSuccessStatusCode) 
                    return (false, response.StatusCode.ToString(), new List<InventoryResponse>());
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "", new List<InventoryResponse>());

                var _response = JsonConvert.DeserializeObject<List<InventoryResponse>>(result);
                if (_response == null) 
                    return (false, "Unable to login at this time", new List<InventoryResponse>());
                //var remoteFileList = _response.Select(r => r.FileName).ToList();

                return (true, "", _response);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (false, ex.Message, new List<InventoryResponse>());
            }
        }

        /// <summary>
        /// gETS INVENTORY FOR ssce AND gIFTED EXAMS
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="year"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, List<InventoryResponse> Data)> GetInventory(string exam, string year, string system)
        {
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization =
                   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());
                var response = await _httpClient.GetAsync($"inventory/{exam}/{year}/{system}");
                if (!response.IsSuccessStatusCode)
                    return (false, response.StatusCode.ToString(), new List<InventoryResponse>());
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "", new List<InventoryResponse>());

                var _response = JsonConvert.DeserializeObject<List<InventoryResponse>>(result);
                if (_response == null)
                    return (false, "Unable to login at this time", new List<InventoryResponse>());
                //var remoteFileList = _response.Select(r => r.FileName).ToList();

                return (true, "", _response);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (false, ex.Message, new List<InventoryResponse>());
            }
        }


        /// <summary>
        /// Gets subjects from server
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="job"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, List<SubjectResponse> Data)> GetSubjects(string exam, string job, string year)
        {
            try
            {
                //subjects/bece/scanning/Obj/2023
                //_httpClient.DefaultRequestHeaders.Authorization =
                //   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());
                var response = await _httpClient.GetAsync($"subjects/{exam}/scanning/{job}/{year}");
                if (!response.IsSuccessStatusCode)
                    return (false, response.StatusCode.ToString(), new List<SubjectResponse>());
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "", new List<SubjectResponse>());

                var _response = JsonConvert.DeserializeObject<List<SubjectResponse>>(result);
                if (_response == null)
                    return (false, "Unable to login at this time", new List<SubjectResponse>());
                //var remoteFileList = _response.Select(r => r.FileName).ToList();

                return (true, "", _response);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (false, ex.Message, new List<SubjectResponse>());
            }
        }

        /// <summary>
        /// Saves scan file to server
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="job"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message)> SaveToServer(string exam, string job, ScanDataRequest Data)
        {
            try
            {
                ///var SaveDataUri = $@"{SaveScannedDataUri}/{data.ExamType.Substring(0,4)}/{data.Job}/save/ext";
                _httpClient.DefaultRequestHeaders.Authorization =
                   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());

                string strPayload = JsonConvert.SerializeObject(Data);
                HttpContent _content = new StringContent(strPayload, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync($"scanning/{exam}/{job}/save/ext", _content);

                //var response = await _httpClient.GetAsync($"scanning/{exam}/{job}/save/ext");
                if (!response.IsSuccessStatusCode)
                    return (false, response.StatusCode.ToString());
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "");

                var _response = JsonConvert.DeserializeObject<ScanDataResponse>(result);
                if (_response == null)
                    return (false, "Unable to save file to the server at this time");
                //var remoteFileList = _response.Select(r => r.FileName).ToList();

                return (true,  _response.Message);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Saves lis of scan files to server
        /// </summary>
        /// <param name="exam"></param>
        /// <param name="job"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message)> SaveToServer(string exam, string job, List<ScanDataRequest> Data)
        {
            try
            {
                ///var SaveDataUri = $@"{SaveScannedDataUri}/{data.ExamType.Substring(0,4)}/{data.Job}/save/ext";
                _httpClient.DefaultRequestHeaders.Authorization =
                   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());

                string strPayload = JsonConvert.SerializeObject(Data);
                HttpContent _content = new StringContent(strPayload, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync($"scanning/{exam}/{job}/save/ext/all", _content);

                //var response = await _httpClient.GetAsync($"scanning/{exam}/{job}/save/ext");
                if (!response.IsSuccessStatusCode)
                    return (false, response.StatusCode.ToString());
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "");

                var _response = JsonConvert.DeserializeObject<ScanDataResponse>(result);
                if (_response == null)
                    return (false, "Unable save files to the server at this time");
                //var remoteFileList = _response.Select(r => r.FileName).ToList();

                return (true, _response.Message);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (false, ex.Message);
            }
        }
    }
}
