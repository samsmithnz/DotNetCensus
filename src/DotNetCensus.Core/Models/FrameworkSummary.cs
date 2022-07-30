using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCensus.Core.Models
{
    public class FrameworkSummary
    {
        public string Framework { get; set; }
        public string FrameworkFamily { get; set; }
        public int Count { get; set; }
    }
}
