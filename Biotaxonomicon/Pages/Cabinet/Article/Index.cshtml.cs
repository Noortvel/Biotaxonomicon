using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace Biotaxonomicon.Pages.Article
{
    public class IndexModel : PageModel
    {
        public List<Models.Article> articles = new List<Models.Article>();
        public List<Models.User> users = new List<Models.User>();
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            articles = Models.Article.GetAll();
            articles.Sort((a,b) => a.Id.CompareTo(b.Id));
            foreach (var x in articles)
            {
                var user = new Models.User { Id = x.UserId };
                user.GetUserLoginNickById();
                users.Add(user);
            }
            return Page();
        }
    }
}