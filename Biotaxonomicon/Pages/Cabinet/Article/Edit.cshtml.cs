using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Biotaxonomicon.Services;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.Article
{
    public class EditModel : PageModel
    {
        public string WarningInfo
        {
            get;
            private set;
        } = "";
        public string SuccesInfo
        {
            get;
            private set;
        } = "";
        public Models.Article Article;
        public IActionResult OnGet(int id)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            var article = Models.Article.GetById(id);
            if(article == null)
            {
                return RedirectToPage("./Index");
            }
            this.Article = article;
            return Page();
        }
        public void OnPost(int? id, string headerInput, string headerLatinInput, string bodyInput, string tagsIdsInput)
        {
            if (id.HasValue && headerInput != null && headerLatinInput != null && bodyInput != null)//&& tagsIdsInput != null
            {
                var sanitizer = new HtmlSanitizer();
                var sbody = sanitizer.Sanitize(bodyInput);
                var parser = new HtmlParser();
                var result = parser.ParseDocument(sbody);

                var cleaner = new CleanUpArticleText(result);
                var content = cleaner.cleanedText;

                tagsIdsInput = "";
                var tagsAr = tagsIdsInput.Split(" ");
                var tags = new List<int>(tagsAr.Length);
                //foreach (var x in tagsAr)
                //{
                //    tags.Add(int.Parse(x));
                //}


                var article = new Models.Article { Id = id.Value, Body = content, Header = headerInput, HeaderLatin = headerLatinInput };

                //int userId = (int)HttpContext.Session.GetInt32("auid");

                article.Update(tags);
                Article = article;
                SuccesInfo = StringResources.StringResources.Instance.SuccesCreated;
            }
        }
    }
}