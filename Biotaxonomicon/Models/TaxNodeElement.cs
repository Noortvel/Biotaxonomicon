using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    [Serializable]
    public class TaxNodeElement
    {
        public List<TaxNodeElement> Childs = new List<TaxNodeElement>();
        public TaxNode Node;
        public Article Article;
        public bool isVisited;
        public TaxNodeElement(TaxNode node, Article article)
        {
            Node = node;
            Article = article;
        }
    }
}
