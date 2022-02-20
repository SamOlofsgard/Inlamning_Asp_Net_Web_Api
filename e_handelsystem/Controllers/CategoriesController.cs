using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_handelsystem;
using e_handelsystem.Models.Entities;
using e_handelsystem.Filters;

namespace e_handelsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SqlContext _context;

        public CategoriesController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [UseApiKey]
        public async Task<ActionResult<IEnumerable<CategoriesEntity>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [UseApiKey]
        public async Task<ActionResult<CategoriesEntity>> GetCategoriesEntity(int id)
        {
            var categoriesEntity = await _context.Categories.FindAsync(id);

            if (categoriesEntity == null)
            {
                return NotFound();
            }

            return categoriesEntity;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> PutCategoriesEntity(int id, CategoriesEntity categoriesEntity)
        {
            if (id != categoriesEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriesEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesEntityExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [UseAdminApiKey]
        public async Task<ActionResult<CategoriesEntity>> PostCategoriesEntity(CategoriesEntity categoriesEntity)
        {
            _context.Categories.Add(categoriesEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriesEntity", new { id = categoriesEntity.Id }, categoriesEntity);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriesEntity(int id)
        {
            var categoriesEntity = await _context.Categories.FindAsync(id);
            if (categoriesEntity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoriesEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriesEntityExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
