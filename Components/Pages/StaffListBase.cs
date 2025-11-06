using Microsoft.AspNetCore.Components;
using StaffClient.Data;
using StaffClient.Services;

namespace StaffClient.Components.Pages;

public class StaffListBase : ComponentBase{
  [Inject]
  public IStaffService StaffService { get; set; }
  public IEnumerable<Employee> employees { get; set; }

  protected override async Task OnInitializedAsync(){
    employees = await StaffService.GetEmployees();
  }
}