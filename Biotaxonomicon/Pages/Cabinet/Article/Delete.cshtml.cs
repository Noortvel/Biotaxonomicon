using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.Article
{
    public class DeleteModel : PageModel
    {
        public int ID;
        public IActionResult OnGet(int id)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            var article = Models.Article.GetById(id);
            if (article == null)
            {
                return RedirectToPage("./Index");
            }
            ID = id;
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            var article = Models.Article.GetById(id);
            if (article == null)
            {
                return RedirectToPage("./Index");
            }
            article.Delete();

            return RedirectToPage("./Index");
        }
    }
}