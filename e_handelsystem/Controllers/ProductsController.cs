using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_handelsystem;
using e_handelsystem.Models.Entities;
using e_handelsystem.Models;
using e_handelsystem.Filters;

namespace e_handelsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [UseApiKey]
        public async Task<ActionResult<IEnumerable<ProductsEntity>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [UseApiKey]
        public async Task<ActionResult<ProductsEntity>> GetProductsEntity(int id)
        {
            var productsEntity = await _context.Products.FindAsync(id);

            if (productsEntity == null)
            {
                return NotFound();
            }

            return productsEntity;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> PutProductsEntity(int id, ProductUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var productsEntity = _context.Products.FirstOrDefault(x => x.Id == model.Id);

            productsEntity.Name = model.Name;
            productsEntity.Description = model.Description;
            productsEntity.Price = model.Price;
            productsEntity.Stock = model.Stock;

            

            var category = _context.Categories.FirstOrDefault(x => x.Name == model.Category.Name);
            if (category != null)
                productsEntity.CategoryId = category.Id;
            else
                productsEntity.Category = new CategoriesEntity(model.Category.Name);




            _context.Entry(productsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [UseAdminApiKey]
        [ActionName("GetProductEntity")]

        public async Task<ActionResult<ProductsEntity>> PostProductsEntity(ProductCreateModel model)
        {
            DateTime now = DateTime.Now;

            var productEntity = new ProductsEntity(model.BarCode, model.Name, model.Description, now, model.Price, model.Currency, model.Stock);


            var category =  _context.Categories.FirstOrDefault(x => x.Name == model.Category.Name);
            if (category != null)
                productEntity.CategoryId = category.Id;
            else
                productEntity.Category = new CategoriesEntity(model.Category.Name);


            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductEntity", new { id = productEntity.Id }, new ProductModel(
                productEntity.Id,
                productEntity.BarCode,
                productEntity.Name,
                productEntity.Description,
                productEntity.Created,
                productEntity.Price,
                productEntity.Currency,
                productEntity.Stock,
                productEntity.CategoryId,
                new CategoriesCreateModel(productEntity.Category.Name)
            ));

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> DeleteProductsEntity(int id)
        {
            var productsEntity = await _context.Products.FindAsync(id);
            if (productsEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productsEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
