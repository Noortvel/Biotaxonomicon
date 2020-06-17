using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.TaxNode
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
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }

            return Page();
        }
        public IActionResult OnPost(int? parentArticleId, int? currentArticleId)
        {
            if (parentArticleId != null && currentArticleId != null)
            {
                SuccesInfo = StringResources.StringResources.Instance.SuccesCreated;
                var node = new Models.TaxNode {  ArticleId = currentArticleId.Value };
                try
                {
                    node.Create(parentArticleId.Value);
                }
                catch (InfoException e)
                {
                    SuccesInfo = "";
                    WarningInfo = e.Message;
                }

            }
            else {
                WarningInfo = StringResources.StringResources.Instance.ErrorCreate;
            }
            return Page();
        }
    }
}