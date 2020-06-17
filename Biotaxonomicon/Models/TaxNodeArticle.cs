using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    [Serializable]
    public struct TaxNodeArticle
    {
        public string HeaderLatin { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public int TaxNodeId { get; set; }
        public int TaxNodeParentId { get; set; }
    }
}
