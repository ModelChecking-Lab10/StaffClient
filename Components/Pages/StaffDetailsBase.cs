using Microsoft.AspNetCore.Components;
using StaffClient.Models;
using StaffClient.Services;

namespace StaffClient.Components.Pages;

public class StaffDetailsBase : ComponentBase
{
  public Staff staff { get; set; } = new Staff();

  [Inject]
  public IStaffService staffService { get; set; }

  [Inject]
  public NavigationManager navigation { get; set; }

  [Parameter]
  public string Id { get; set; }

  protected override async Task OnInitializedAsync()
  {
    Id = Id ?? "1";
    staff = await staffService.GetStaff(int.Parse(Id));
  }

  public async Task DeleteStaff()
  {
    await staffService.DeleteStaff(int.Parse(Id));
    navigation.NavigateTo("/stafflist", forceLoad: true);
  }
}