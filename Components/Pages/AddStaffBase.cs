using Microsoft.AspNetCore.Components;
using StaffClient.Data;
using StaffClient.Services;

namespace StaffClient.Components.Pages;

public class AddStaffBase : ComponentBase
{
    public Employee employee { get; set; } = new Employee();

    [Inject]
    public IStaffService staffService { get; set; }

    [Inject]
    public NavigationManager navigation { get; set; }

    public async Task HandleValidSubmit()
    {
        await staffService.AddEmployee(employee);
        navigation.NavigateTo("/stafflist", forceLoad: true);
    }
}