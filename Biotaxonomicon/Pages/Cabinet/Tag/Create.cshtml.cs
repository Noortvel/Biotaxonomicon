using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.Tag
{
    public class CreateModel : PageModel
    {
        public string WarningInfo
        {
            private set;
            get;
        } = "";
        public string SuccesInfo
        {
            private set;
            get;
        } = "";
        public IActionResult OnPost(string latinNameInput, string russianNameInput)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            if (latinNameInput != null && russianNameInput != null)
            {
                var tag = new Models.Tag { LatinName = latinNameInput, RussianName = russianNameInput };
                try
                {
                    tag.Create();
                }
                catch(InfoException e)
                {
                    WarningInfo = e.Message;
                    return Page();
                }
                SuccesInfo = StringResources.StringResources.Instance.SuccesCreated;
            }
            return Page();
        }
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            return Page();
        }
    }
}