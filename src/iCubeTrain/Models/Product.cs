using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Models
{
    public class Product
    {

        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }
    }
}