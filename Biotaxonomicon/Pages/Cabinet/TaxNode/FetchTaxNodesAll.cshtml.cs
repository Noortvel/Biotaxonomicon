using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.TaxNode
{
    public class FetchTaxNodesAllModel : PageModel
    {
        public IActionResult OnGet()
        {
            //if (!HttpContext.Session.Keys.Contains("auid"))
            //{
            //    return RedirectToPage("/Auth/Login");
            //}
            var nodeList = Models.TaxNode.GetAllNodes();
            var nodeArticlesElems = new List<Models.TaxNodeArticle>();
            foreach (var x in nodeList)
            {
                var article = Models.Article.GetById(x.ArticleId);
                var node = new Models.TaxNodeArticle();
                node.Body = article.Body;
                node.Header = article.Header;
                node.HeaderLatin = article.HeaderLatin;
                node.TaxNodeId = x.Id;
                node.TaxNodeParentId = x.ParentNodeId;
                nodeArticlesElems.Add(node);
            }
            return new JsonResult(nodeArticlesElems);
        }
    }
}