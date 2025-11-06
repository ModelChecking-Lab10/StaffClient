using System.Text;
using System.Text.Json;
using StaffClient.Data;

namespace StaffClient.Services;

public class StaffService : IStaffService
{
    private readonly HttpClient httpClient;

    public StaffService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await httpClient.GetFromJsonAsync<Employee[]>("api/Employee");
    }

    public async Task<Employee> GetEmployee(int id)
    {
        return await httpClient.GetFromJsonAsync<Employee>($"api/Employee/{id}");
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        var response = await httpClient.PostAsJsonAsync($"api/Employee", employee);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Employee>();
    }

    public async Task<Employee> UpdateEmployee(int id, Employee employee)
    {
        var response = await httpClient.PutAsJsonAsync($"api/Employee/{id}", employee);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Employee>();
    }

    public async Task<Employee> DeleteEmployee(int id)
    {
        return await httpClient.DeleteFromJsonAsync<Employee>($"api/Employee/{id}");
    }
}