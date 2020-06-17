using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biotaxonomicon
{
    public static class HtmlHelpersExtension
    {
        public static string ShowSubItems(this IHtmlHelper helper, Models.TaxNodeElement elem)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<li>");
            output.Append(elem.Article.Header);
            if (elem.Node.ParentNodeId > 0)
            {
                var btn1 = $"<a class=\"btn - sm btn - secondary\" asp-page=\"./ Edit\" asp-route-id=\"{elem.Node.Id}\" role=\"button\">Редактировать</a>";
                var btn2 = $"<a class=\"btn - sm btn - danger\" asp-page=\"./ Delete\" asp-route-id=\"{elem.Node.Id}\" role=\"button\">Удалить</a>";
                output.Append(btn1);
                output.Append(btn2);
            }
            if(elem.Childs.Count > 0)
            {
                output.Append("<ul>");
                foreach(var x in elem.Childs)
                {
                    output.Append(ShowSubItems(helper, x));
                }
                output.Append("</ul>");

            }
            output.Append("</li>");
            return output.ToString();
        }
    }
}
