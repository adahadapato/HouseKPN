using HouseKPN.Dto;
using HouseKPN.Models;

namespace HouseKPN.Resources.Interfaces
{
    public interface IResourceService
    {
        Task<(bool Success, string Message, LoginResponse Data)> Login(LoginRequest data);
        Task<(bool Success, string Message, StaffObject? Data)> GetStaffDetails(string _staffNo);
        Task<(bool Success, string Message, List<InventoryResponse> Data)> GetInventory(string exam, string job, string year, string system);
        Task<(bool Success, string Message, List<InventoryResponse> Data)> GetInventory(string exam, string year, string system);
        Task<(bool Success, string Message, List<SubjectResponse> Data)> GetSubjects(string exam, string job, string year);
        Task<(bool Success, string Message)> SaveToServer(string exam, string job, ScanDataRequest Data);
        Task<(bool Success, string Message)> SaveToServer(string exam, string job, List<ScanDataRequest> Data);
    }
}
