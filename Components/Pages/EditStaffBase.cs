using Microsoft.AspNetCore.Components;
using StaffClient.Data;
using StaffClient.Services;

namespace StaffClient.Components.Pages;

public class EditStaffBase : ComponentBase
{
    public Employee employee { get; set; } = new Employee();

    [Inject]
    public IStaffService staffService { get; set; }

    [Inject]
    public NavigationManager navigation { get; set; }

    [Parameter]
    public String Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Id = Id ?? "1";
        employee = await staffService.GetEmployee(int.Parse(Id));
    }

    public async Task HandleValidSubmit()
    {
        await staffService.UpdateEmployee(int.Parse(Id), employee);
        navigation.NavigateTo("/stafflist", forceLoad: true);
    }
}