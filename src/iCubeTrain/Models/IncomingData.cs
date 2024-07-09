using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Models
{
    public class IncomingData
    {
        public List<TagValueData> Data { get; set; }
    }

    public class TagValueData
    {
        public string Tagname { get; set; }
        public string? Description { get; set; }
        public string? SubDescription { get; set; }
        public string? Value { get; set; }
        public string? Unit { get; set; }
        public int Quality { get; set; }
        public int Status { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}