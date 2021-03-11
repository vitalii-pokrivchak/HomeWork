using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using HomeWork.Data;
using HomeWork.Models;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeWork.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly AzStorageService _service;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateUserModel> _logger;

        [BindProperty]
        public User UserModel { get; set; }

        public CreateUserModel(ILogger<CreateUserModel> logger, AzStorageService service, ApplicationDbContext context)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _context.Users.AddAsync(UserModel);
                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"User Created : {UserModel.ToString()}");
                    return RedirectToPage("Index");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _logger.LogError(new EventId(1000, "CreateUserModel"), e, "CreateUserModel exception");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnGetUploadAsync()
        {
            var users = await _context.Users.ToListAsync();
            if (users.Count > 0)
            {
                var json = JsonSerializer.Serialize<List<User>>(users);
                var stream = new MemoryStream(await GetUserInStream(json));
                await _service.UploadData(stream);
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("CreateUser");
            }
        }

        private async Task<byte[]> GetUserInStream(string data)
        {
            if (data != "")
            {
                using (var ms = new MemoryStream())
                using (var sw = new StreamWriter(ms))
                {
                    await sw.WriteAsync(data);
                    await sw.FlushAsync();
                    return ms.ToArray();
                }
            }
            return null;
        }
    }
}