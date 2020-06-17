using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Services
{
    public class CleanUpArticleText
    {
        public string cleanedText;
        public CleanUpArticleText(IHtmlDocument document)
        {
            var ahrefs = document.GetElementsByTagName("a");
            foreach (var x in ahrefs)
            {
                x.ClearAttr();
            }
            var spans = document.GetElementsByTagName("span");
            foreach (var x in spans)
            {
                x.ClearAttr();
            }
            var sups = document.GetElementsByTagName("sup");
            foreach (var x in sups)
            {
                x.Parent.RemoveChild(x);
            }
            cleanedText = document.Body.InnerHtml;
            cleanedText = cleanedText.Replace("<span>", "<i>");
            cleanedText = cleanedText.Replace("</span>", "</i>");
        }
    }
}
