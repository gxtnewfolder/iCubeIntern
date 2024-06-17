using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Models;
using iCubeTrain.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iCubeTrain.Controllers
{
    [Route("api/products")] // localhost:xxxx/api/Product
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await unitOfWork.Products.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await unitOfWork.Products.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await unitOfWork.Products.AddAsync(product);
            await unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await unitOfWork.Products.UpdateAsync(product);
            await unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await unitOfWork.Products.DeleteAsync(id);
            await unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}