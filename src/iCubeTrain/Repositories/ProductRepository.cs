using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Data;
using iCubeTrain.Models;
using iCubeTrain.Repositories.Generic;
using iCubeTrain.Repositories.Interface;

namespace iCubeTrain.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    // Implement specific methods for products if needed
}
}