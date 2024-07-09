using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Models
{
    public class TransformedData
    {
        public Dictionary<string, List<TagValue>>? Data { get; set; }
    }

    public class TagValue
    {
        public string? Value { get; set; }
        public int Quality { get; set; }
        public int Status { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}