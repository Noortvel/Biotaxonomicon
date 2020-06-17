using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.TaxNode
{
    public class IndexModel : PageModel
    {
        public TaxNodeElement Root = null;
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            var nodeList = Models.TaxNode.GetAllNodes();
            var nodeElems = new List<TaxNodeElement>();
            foreach (var x in nodeList)
            {
                var article = Models.Article.GetById(x.ArticleId);
                var node = new TaxNodeElement(x, article);
                nodeElems.Add(node);
            }
            TaxNodeElement root = null;
            foreach (var x in nodeElems)
            {
                var parent = nodeElems.Find((el) => el.Node.Id == x.Node.ParentNodeId);
                if (parent != null)
                {
                    parent.Childs.Add(x);
                }
                else
                {
                    root = x;
                }
            }

            root = nodeElems.Find((obj)=> obj.Node.ParentNodeId <= 0);
            Root = root;

            return Page();
        }
    }
}