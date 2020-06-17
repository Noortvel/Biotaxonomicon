using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace Biotaxonomicon.Pages.Cabinet.Article
{
    public class FetchArticlesAllModel : PageModel
    {
        public IActionResult OnGet(string articleHeader)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            if (articleHeader != null)
            {
                var sqlCmd = $"select id,latin_header, russian_header from articles where lower(latin_header) like '%{articleHeader.ToLower()}%' or lower(russian_header) like '%{articleHeader.ToLower()}%'";
                var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
                var reader = cmd.ExecuteReader();
                List<Models.Article> articles = new List<Models.Article>();
                while (reader.Read())
                {
                    var artcl = new Models.Article();
                    artcl.Id = reader.GetInt32(0);
                    artcl.HeaderLatin = reader.GetString(1);
                    artcl.Header = reader.GetString(2);
                    articles.Add(artcl);
                }
                reader.Close();
                cmd.Dispose();
                return new JsonResult(articles);
            }
            return Page();
        }

    }
}