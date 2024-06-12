using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Models
{
    public class NewUserModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}