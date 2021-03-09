using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using HomeWork.Models;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly AzStorageService _service;

        [BindProperty]
        public User UserModel { get; set; }

        public CreateUserModel(AzStorageService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                UserModel.Id = Guid.NewGuid();
                string json = JsonSerializer.Serialize<User>(UserModel, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All))
                });
                await _service.UploadData(json);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}