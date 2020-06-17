using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace Biotaxonomicon.Pages.Cabinet.Tag
{
    public class FetchTaxNodesTagsModel : PageModel
    {
        public IActionResult OnGet(string tagStr)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            if (tagStr != null)
            {
                var sqlCmd = $"select id,latin_name, russian_name from tags where " +
                    $"((lower(latin_name) like '%{tagStr.ToLower()}%' " +
                    $"or lower(russian_name) like '%{tagStr.ToLower()}%')" +
                    $"and EXISTS(select id from tags_tax_nodes where tag_id = tags.id))";
                var cmd = new NpgsqlCommand(sqlCmd, DBConnect.Connection);
                var reader = cmd.ExecuteReader();
                List<Models.Tag> taglist = new List<Models.Tag>();
                while (reader.Read())
                {
                    var tag = new Models.Tag();
                    tag.Id = reader.GetInt32(0);
                    tag.LatinName = reader.GetString(1);
                    tag.RussianName = reader.GetString(2);
                    taglist.Add(tag);
                }
                reader.Close();
                cmd.Dispose();
                return new JsonResult(taglist);
            }
            return Page();
        }
    }
}