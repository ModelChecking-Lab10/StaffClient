using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StaffClient.Models;
using StaffClient.Services;
using System.IO;
using System.Threading.Tasks;

namespace StaffClient.Components.Pages
{
    public class AddStaffBase : ComponentBase
    {
        public Staff staff { get; set; } = new Staff();

        protected string? photoPreview;

        private IBrowserFile? selectedFile;

        [Inject] public IStaffService staffService { get; set; }
        [Inject] public NavigationManager navigation { get; set; }

        public async Task HandleValidSubmit()
        {
            string photoPath;

            if (selectedFile != null)
            {
                photoPath = await staffService.UploadPhoto(selectedFile);

                if (string.IsNullOrEmpty(photoPath))
                {
                    Console.WriteLine("Upload file thất bại, không thể thêm nhân viên.");
                    return;
                }
            }
            else
            {
                photoPath = "uploads/default-avatar.png";
            }

            staff.Photo = photoPath;

            await staffService.AddStaff(staff);

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