using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.Tag
{
    public class IndexModel : PageModel
    {
        public List<Models.Tag> tags = new List<Models.Tag>();
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            tags = Models.Tag.GetAll();
            return Page();
        }
    }
}