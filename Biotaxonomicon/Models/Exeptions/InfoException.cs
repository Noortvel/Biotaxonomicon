using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biotaxonomicon.Models.Exeptions
{
    public class InfoException : Exception
    {
        public InfoException(string message) : base(message)
        {
        }
    }
}
