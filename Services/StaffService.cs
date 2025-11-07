using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using StaffClient.Models;

namespace StaffClient.Services;

public class StaffService : IStaffService
{
    private readonly HttpClient httpClient;

    public StaffService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Staff>> GetStaffs()
    {
        return await httpClient.GetFromJsonAsync<Staff[]>("api/Staff");
    }

    public async Task<Staff> GetStaff(int id)
    {
        return await httpClient.GetFromJsonAsync<Staff>($"api/Staff/{id}");
    }

    public async Task<Staff> AddStaff(Staff staff)
    {
        var response = await httpClient.PostAsJsonAsync("api/Staff", staff);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Staff>();
    }

    public async Task<Staff> UpdateStaff(int id, Staff staff)
    {
        var response = await httpClient.PutAsJsonAsync($"api/Staff/{id}", staff);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Staff>();
    }

    public async Task<Staff> DeleteStaff(int id)
    {
        var response = await httpClient.DeleteAsync($"api/Staff/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Staff>();
    }

    public async Task<string> UploadPhoto(IBrowserFile file)
    {
        try
        {
            // Tạo nội dung form multipart
            using var content = new MultipartFormDataContent();

            // Mở stream từ IBrowserFile
            // Giới hạn file 10MB
            using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);

            // Tạo StreamContent
            using var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            // Thêm file vào form data
            content.Add(content: streamContent, name: "\"file\"", fileName: file.Name);

            // Gọi API Upload
            var response = await httpClient.PostAsync("api/upload", content);

            if (response.IsSuccessStatusCode)
            {
                // Đọc phản hồi JSON { "path": "uploads/file.jpg" }
                var result = await response.Content.ReadFromJsonAsync<UploadResult>();
                return result.path;
            }
            else
            {
                // Xử lý lỗi
                Console.WriteLine($"Error uploading file: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception uploading file: {ex.Message}");
            return null;
        }
    }

    // Lớp private nhỏ để đọc kết quả JSON từ API
    private class UploadResult
    {
        public string path { get; set; }
    }
}