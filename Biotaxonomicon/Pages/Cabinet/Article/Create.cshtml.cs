using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using Biotaxonomicon.Services;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Article
{
    public class CreateModel : PageModel
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
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            return Page();
        }
        public void OnPost(string headerInput, string headerLatinInput, string bodyInput, string tagsIdsInput)
        {
            if(headerInput != null && headerLatinInput != null && bodyInput != null && tagsIdsInput != null)
            {
                var sanitizer = new HtmlSanitizer();
                var sbody = sanitizer.Sanitize(bodyInput);
                var parser = new HtmlParser();
                var result = parser.ParseDocument(sbody);

                var cleaner = new CleanUpArticleText(result);
                var content = cleaner.cleanedText;



                var tagsAr = tagsIdsInput.Split(" ");
                var tags = new List<int>(tagsAr.Length);
                foreach(var x in tagsAr)
                {
                    tags.Add(int.Parse(x));
                }


                var article = new Models.Article { Body = content, Header = headerInput, HeaderLatin = headerLatinInput };
                int userId = (int)HttpContext.Session.GetInt32("auid");
                SuccesInfo = StringResources.StringResources.Instance.SuccesCreated;
                try
                {
                    article.Create(userId, tags);
                } 
                catch(Exception e)
                {
                    SuccesInfo = "";
                    WarningInfo = StringResources.StringResources.Instance.DataError;
                }
            }
        }
    }
}