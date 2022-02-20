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
    public class OrdersController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        [UseApiKey]
        public async Task<ActionResult<IEnumerable<OrdersEntity>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [UseApiKey]
        public async Task<ActionResult<OrdersEntity>> GetOrdersEntity(int id)
        {
            var ordersEntity = await _context.Orders.FindAsync(id);

            if (ordersEntity == null)
            {
                return NotFound();
            }

            return ordersEntity;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [UseApiKey]
        public async Task<IActionResult> PutOrdersEntity(int id, OrderUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            DateTime now = DateTime.Now;
            var _order = await _context.Orders.FindAsync(model.Id);

            _order.TotalPrice = model.TotalPrice;
            _order.Status = model.Status;

            
            _context.Entry(_order).State = EntityState.Modified;




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersEntityExists(id))
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

        // POST: api/Orders
        // Kollar först om det finns en order från samma kund, om det finns det så läggs det bara till nya orderRows i den befintliga ordern annars skapas det en ny order 
        [HttpPost]
        [UseApiKey]
        public async Task<ActionResult<OrderCreateModel>> PostOrdersEntity(OrderInputModel model)
        {
            DateTime now = DateTime.Now;

            var _order =  _context.Orders.FirstOrDefault(x => x.CustomerId == model.CustomerId);
            if (_order != null)
            {
                var _totalPrice = _order.TotalPrice + model.Price;

                var _OrderUpdate = new OrderUpdateModel(_order.Id, _totalPrice, model.Status);
                await PutOrdersEntity(_order.Id, _OrderUpdate);
                var orderRowEntity = new OrderRowEntity(_order.Id, model.ProductId, model.Quantity, model.Price);
                _context.OrderRows.Add(orderRowEntity);
                await _context.SaveChangesAsync();                
                return NoContent();
            }



            var ordersEntity = new OrdersEntity(model.CustomerId, model.CustomerName, model.CustomerAddress, now, model.Price, model.Status);
            //ordersEntity.OrderRow = new OrderRowEntity(model.ProductId, model.Quantity, model.Price);

            _context.Orders.Add(ordersEntity);   
            await _context.SaveChangesAsync();

            var _orderRowId = await _context.Orders.FirstOrDefaultAsync(x => x.CustomerName == model.CustomerName);

            var _orderRow = new OrderRowEntity(_orderRowId.Id,model.ProductId, model.Quantity, model.Price);

            _context.OrderRows.Add(_orderRow);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetOrdersEntity", new { id = ordersEntity.Id }, new OrderCreateModel(
                ordersEntity.Id,
                ordersEntity.CustomerId,
                ordersEntity.CustomerName,
                ordersEntity.CustomerAddress,
                ordersEntity.OrderDate,
                ordersEntity.TotalPrice,
                ordersEntity.Status            ));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [UseApiKey]
        public async Task<IActionResult> DeleteOrdersEntity(int id)
        {
            var ordersEntity = await _context.Orders.FindAsync(id);
            if (ordersEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(ordersEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
