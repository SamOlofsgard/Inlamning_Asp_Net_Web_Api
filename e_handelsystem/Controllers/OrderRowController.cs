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
    public class OrderRowController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderRowController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/OrderRow
        [HttpGet]
        [UseApiKey]
        public async Task<ActionResult<IEnumerable<OrderRowEntity>>> GetOrderRows()
        {
            return await _context.OrderRows.ToListAsync();
        }

        // GET: api/OrderRow/5
        [HttpGet("{id}")]
        [UseApiKey]
        public async Task<ActionResult<OrderRowEntity>> GetOrderRowEntity(int id)
        {
            var orderRowEntity = await _context.OrderRows.FindAsync(id);

            if (orderRowEntity == null)
            {
                return NotFound();
            }

            return orderRowEntity;
        }

        // PUT: api/OrderRow/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [UseApiKey]
        public async Task<IActionResult> PutOrderRowEntity(int id, OrderRowUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            
            var _orderRow = await _context.OrderRows.FindAsync(model.Id);

            _orderRow.Quantity = model.Quantity;
            _orderRow.Price = model.Price;


            _context.Entry(_orderRow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRowEntityExists(id))
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

        // POST: api/OrderRow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [UseApiKey]
        public async Task<ActionResult<OrderRowEntity>> PostOrderRowEntity(OrderRowInputModel model)
        {
            var orderRowEntity = new OrderRowEntity(model.OrderId, model.ProductId, model.Quantity, model.Price);

            _context.OrderRows.Add(orderRowEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderRowEntity", new { id = orderRowEntity.Id }, new OrderRowEntity(orderRowEntity.Id, orderRowEntity.OrderId, orderRowEntity.ProductId, orderRowEntity.Quantity, orderRowEntity.Price));
        }


        // DELETE: api/OrderRow/5
        [HttpDelete("{id}")]
        [UseApiKey]
        public async Task<IActionResult> DeleteOrderRowEntity(int id)
        {
            var orderRowEntity = await _context.OrderRows.FindAsync(id);
            if (orderRowEntity == null)
            {
                return NotFound();
            }

            _context.OrderRows.Remove(orderRowEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderRowEntityExists(int id)
        {
            return _context.OrderRows.Any(e => e.Id == id);
        }
    }
}
