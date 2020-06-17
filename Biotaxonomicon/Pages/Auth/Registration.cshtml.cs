using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models;
using Biotaxonomicon.Models.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace Biotaxonomicon.Pages.Auth
{
    public class RegistrationModel : PageModel
    {
        public string WarningInfo
        {
            get;
            private set;
        } = "";
        public IActionResult OnGet()
        {
            if (HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Index");
            }
            WarningInfo = "";
            return Page();
        }
        public IActionResult OnPost(string nickInput, string loginInput, string passwordInput)
        {

            if (loginInput != null && passwordInput != null && passwordInput != null)
            {
                
                if(loginInput.Length < 6)
                {
                    WarningInfo = StringResources.StringResources.Instance.Login.TooShort;
                    return Page();
                }
                if(passwordInput.Length < 6)
                {
                    WarningInfo = StringResources.StringResources.Instance.Password.TooShort;
                    return Page();
                }
                if(nickInput.Length < 6)
                {
                    WarningInfo = StringResources.StringResources.Instance.Nick.TooShort;
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
                if (nickInput.Length > 24)
                {
                    WarningInfo = StringResources.StringResources.Instance.Nick.TooLong;
                    return Page();
                }
                var user = 
                    new User { Nick = nickInput, Login = loginInput, PasswordHash = passwordInput };
                try
                {
                    user.Create();
                }
                catch (InfoException e)
                {
                    WarningInfo = e.Message;
                }
                if (WarningInfo == "")
                {
                    return RedirectToPage("/Auth/Login");
                }
            }
            return Page();
        }
    }
}