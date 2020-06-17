using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models
{
    public class TaxNodeTag
    {
        public int Id { get; set; }
        public int TaxNodeId { get; set; }
        public int TagId { get; set; }
    }
}
