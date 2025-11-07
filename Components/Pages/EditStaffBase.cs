using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StaffClient.Models;
using StaffClient.Services;
using System.IO;
using System.Threading.Tasks;

namespace StaffClient.Components.Pages
{
    public class EditStaffBase : ComponentBase
    {
        public Staff staff { get; set; } = new Staff();
        protected string? photoPreview;

        private IBrowserFile? selectedFile;

        [Inject] public IStaffService staffService { get; set; }
        [Inject] public NavigationManager navigation { get; set; }

        [Parameter] public string Id { get; set; }

        private string ApiBaseUrl = "http://localhost:5143";

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(Id)) return;

            staff = await staffService.GetStaff(int.Parse(Id));

            if (!string.IsNullOrEmpty(staff.Photo))
            {
                photoPreview = $"{ApiBaseUrl}/{staff.Photo.Replace("\\", "/")}";
            }
        }

        public async Task HandleValidSubmit()
        {
            if (selectedFile != null)
            {
                string newPhotoPath = await staffService.UploadPhoto(selectedFile);

                if (!string.IsNullOrEmpty(newPhotoPath))
                {
                    staff.Photo = newPhotoPath;
                }
                else
                {
                    Console.WriteLine("Lỗi upload ảnh mới.");
                    return;
                }
            }
            await staffService.UpdateStaff(int.Parse(Id), staff);
            navigation.NavigateTo("/stafflist", forceLoad: true);
        }


        protected async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;

            if (selectedFile != null)
            {
                using var stream = selectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10MB
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                photoPreview = $"data:{selectedFile.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}";
            }
        }
    }
}