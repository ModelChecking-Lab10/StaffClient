using Microsoft.AspNetCore.Components;
using StaffClient.Models;
using StaffClient.Services;

namespace StaffClient.Components.Pages;

public class StaffListBase : ComponentBase
{
  [Inject]
  public IStaffService staffService { get; set; }

  public IEnumerable<Staff> staffs { get; set; }

  protected override async Task OnInitializedAsync()
  {
    staffs = await staffService.GetStaffs();
  }
}