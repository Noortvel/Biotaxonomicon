using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace Biotaxonomicon.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public string WarningInfo
        {
            private set;
            get;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Index");
            }
            WarningInfo = "";
            return Page();
        }
        public IActionResult OnPost(string loginInput, string passwordInput)
        {
            if(loginInput != null && passwordInput != null)
            {
                if (loginInput.Length < 6)
                {
                    WarningInfo = StringResources.StringResources.Instance.Login.TooShort;
                    return Page();
                }
                if (passwordInput.Length < 6)
                {
                    WarningInfo = StringResources.StringResources.Instance.Password.TooShort;
                    return Page();
                }
                if (loginInput.Length > 24)
                {
                    WarningInfo = StringResources.StringResources.Instance.Login.TooLong;
                    return Page();
                }
                if (passwordInput.Length > 24)
                {
                    WarningInfo = StringResources.StringResources.Instance.Password.TooLong;
                    return Page();
                }
                var user = new User { Login = loginInput, PasswordHash = passwordInput };
                int lastId = user.SelectLoginPassword();
                if (lastId != -1)
                {
                    HttpContext.Session.SetInt32("auid", lastId);
                    return RedirectToPage("/Cabinet/Index");
                }
            }
            return Page();
        }
    }
}