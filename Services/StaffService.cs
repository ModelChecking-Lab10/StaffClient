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
            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
            using var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            content.Add(content: streamContent, name: "\"file\"", fileName: file.Name);

            var response = await httpClient.PostAsync("api/upload", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UploadResult>();
                return result.path;
            }
            else
            {
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

    private class UploadResult
    {
        public string path { get; set; }
    }
}