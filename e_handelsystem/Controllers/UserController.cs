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
    public class UserController : ControllerBase
    {
        private readonly SqlContext _context;

        public UserController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        [UseApiKey]
        public async Task<ActionResult<IEnumerable<UserOutputModel>>> GetUsers()
        {
            var items = new List<UserOutputModel>();
            foreach (var i in await _context.Users.Include(x => x.Address).ToListAsync())
                items.Add(new UserOutputModel(
                    i.Id,
                    i.FirstName,
                    i.LastName,
                    i.Email,
                    i.Password,
                    new AddressModel(i.Address.AddressLine, i.Address.PostalCode, i.Address.City)
                ));

            return items;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [UseApiKey]
        public async Task<ActionResult<UserOutputModel>> GetUserEntity(int id)
        {
            var userEntity = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity == null)
            {
                return NotFound();
            }

            return new UserOutputModel(
                    userEntity.Id,
                    userEntity.FirstName,
                    userEntity.LastName,
                    userEntity.Email,
                    userEntity.Password,
                    new AddressModel(userEntity.Address.AddressLine, userEntity.Address.PostalCode, userEntity.Address.City)
                );
        }

        // PUT: api/User/5
        // Uppdaterar en användares både namn, lösenord, kontaktinfor och adressinformation
        [HttpPut("{id}")]
        [UseApiKey]
        public async Task<IActionResult> PutUserEntity(int id, UserEntity model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var userEntity = await _context.Users.FindAsync(model.Id);

            userEntity.FirstName = model.FirstName;
            userEntity.LastName = model.LastName;
            userEntity.Email = model.Email;
            userEntity.Password = model.Password;

            var address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressLine == model.Address.AddressLine && x.PostalCode == model.Address.PostalCode);
            if (address != null)
                model.AddressId = address.Id;
            else
                userEntity.Address = new AddressEntity(model.Address.AddressLine, model.Address.PostalCode, model.Address.City);        


            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [UseApiKey]
        [ActionName("GetUser")]
        public async Task<ActionResult<UserOutputModel>> PostUserEntity(UserInputModel model)
        {
            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
                return BadRequest();

            var userEntity = new UserEntity(model.FirstName, model.LastName, model.Email, model.Password);

            var address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressLine == model.AddressLine && x.PostalCode == model.PostalCode);
            if (address != null)
                userEntity.AddressId = address.Id;
            else
                userEntity.Address = new AddressEntity(model.AddressLine, model.PostalCode, model.City);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = userEntity.Id }, new UserOutputModel(
                    userEntity.Id,
                    userEntity.FirstName,
                    userEntity.LastName,
                    userEntity.Email,
                    userEntity.Password,
                    new AddressModel(userEntity.Address.AddressLine, userEntity.Address.PostalCode, userEntity.Address.City)
                ));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> DeleteUserEntity(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
