using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Models
{
    public class ChatHistory
    {
        public int Id { get; set; }
        public string UserQuery { get; set; }
        public string Response { get; set; }
        public DateTime Timestamp { get; set; }
    }
}