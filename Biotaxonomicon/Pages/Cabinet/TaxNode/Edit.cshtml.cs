using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.TaxNode
{
    public class EditModel : PageModel
    {
        public Models.TaxNode TaxNode;
        public Models.Article Article;
        public Models.Article ParentArticle;
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
        public IActionResult OnGet(int id)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            TaxNode = Models.TaxNode.GetById(id);
            Article = Models.Article.GetById(TaxNode.ArticleId);
            if(TaxNode.ParentNodeId > 0)
            {
                var parent = Models.TaxNode.GetById(TaxNode.ParentNodeId);
                ParentArticle = Models.Article.GetById(parent.ArticleId);
            }
            if (TaxNode == null || Article == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
        public IActionResult OnPost(int? id, int? parentArticleId, int? currentArticleId)
        {
            if (id != null && parentArticleId != null && currentArticleId != null)
            {
                SuccesInfo = StringResources.StringResources.Instance.SuccesCreated;
                var node = new Models.TaxNode 
                {Id = id.Value, ArticleId = currentArticleId.Value };
                node.Update(parentArticleId.Value);
            }
            else
            {
                WarningInfo = StringResources.StringResources.Instance.ErrorCreate;
            }
            return RedirectToPage("./Index");
        }
    }
}
