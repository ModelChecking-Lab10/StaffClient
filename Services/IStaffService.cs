using Microsoft.AspNetCore.Components.Forms;
using StaffClient.Models;

namespace StaffClient.Services;

public interface IStaffService
{
    Task<IEnumerable<Staff>> GetStaffs();
    Task<Staff> GetStaff(int id);
    Task<Staff> AddStaff(Staff Staff);
    Task<Staff> UpdateStaff(int id, Staff Staff);
    Task<Staff> DeleteStaff(int id);
    Task<string> UploadPhoto(IBrowserFile file);
}