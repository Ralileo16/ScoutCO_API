using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoutCO_API.Models;

namespace ScoutCO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly ScoutChaudOuestContext _context;

        public ChildrenController(ScoutChaudOuestContext context)
        {
            _context = context;
        }

        // GET: api/Children
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>> GetChildren()
        {
            return await _context.Children
				.Include(c => c.FkParent1Navigation)
				.Include(c => c.FkParent2Navigation)
				.Select(c => new Child
				{
					Id = c.Id,
					FirstName = c.FirstName,
					LastName = c.LastName,
					DateOfBirth = c.DateOfBirth,
					Gender = c.Gender,
					Address = c.Address,
					City = c.City,
					Province = c.Province,
					PostalCode = c.PostalCode,
					Phone = c.Phone,
					Email = c.Email,
					Notes = c.Notes,
					FkParent1 = c.FkParent1,
					FkParent2 = c.FkParent2,
					FkParent1Navigation = c.FkParent1Navigation,
					FkParent2Navigation = c.FkParent2Navigation,
					DateRegistration = c.DateRegistration,
					DatePaid = c.DatePaid,
					IsPaid = c.IsPaid
				})
				.ToListAsync();

		}

		// GET: api/Children/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetChild(int id)
        {
            var child = await _context.Children
                .Include(c => c.FkParent1Navigation)
				.Include(c => c.FkParent2Navigation)
				.Select(c => new Child
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender,
                    Address = c.Address,
                    City = c.City,
                    Province = c.Province,
                    PostalCode = c.PostalCode,
                    Phone = c.Phone,
                    Email = c.Email,
                    Notes = c.Notes,
                    FkParent1 = c.FkParent1,
                    FkParent2 = c.FkParent2,
                    FkParent1Navigation = c.FkParent1Navigation,
                    FkParent2Navigation = c.FkParent2Navigation,
                    DateRegistration = c.DateRegistration,
                    DatePaid = c.DatePaid,
                    IsPaid = c.IsPaid
                })
                .FirstOrDefaultAsync(c => c.Id == id);

            if (child == null)
            {
                return NotFound();
            }

            return child;
        }

        // PUT: api/Children/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChild(int id, Child child)
        {
            if (id != child.Id)
            {
                return BadRequest();
            }

            _context.Entry(child).State = EntityState.Modified;
            if (child.FkParent1Navigation != null && child.FkParent2Navigation != null) 
            {
				_context.Entry(child.FkParent1Navigation).State = EntityState.Modified;
				_context.Entry(child.FkParent2Navigation).State = EntityState.Modified;
			}
			

			try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildExists(id))
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

        // POST: api/Children
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Child>> PostChild(Child child)
        {
            if (FindChild(child))
            {
                return Conflict();
            }
            if (child.FkParent1Navigation != null) { 
                if (!ParentExists(child.FkParent1Navigation))
                {
                    _context.Parents.Add(child.FkParent1Navigation);
					await _context.SaveChangesAsync();
				}
                child.FkParent1 = FindParentID(child.FkParent1Navigation);
			}
			if (child.FkParent2Navigation != null)
			{
				if (!ParentExists(child.FkParent2Navigation))
				{
					_context.Parents.Add(child.FkParent2Navigation);
					await _context.SaveChangesAsync();
				}
				child.FkParent2 = FindParentID(child.FkParent2Navigation);
			}

			_context.Children.Add(child);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChildExists(child.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

			child = new Child
			{
				Id = child.Id,
				FirstName = child.FirstName,
				LastName = child.LastName,
				DateOfBirth = child.DateOfBirth,
				Gender = child.Gender,
				Address = child.Address,
				City = child.City,
				Province = child.Province,
				PostalCode = child.PostalCode,
				Phone = child.Phone,
				Email = child.Email,
				Notes = child.Notes,
				FkParent1 = child.FkParent1,
				FkParent2 = child.FkParent2,
				DateRegistration = child.DateRegistration,
				DatePaid = child.DatePaid,
				IsPaid = child.IsPaid
			};

			return CreatedAtAction("GetChild", new { id = child.Id }, child);
        }

        // DELETE: api/Children/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.Id == id);
        }

		private bool FindChild(Child c)
		{
			return _context.Children.Any(e =>
			e.FirstName == c.FirstName &&
			e.LastName == c.LastName &&
			e.Phone == c.Phone &&
			e.Email == c.Email &&
            e.DateOfBirth == c.DateOfBirth);
		}

		private bool ParentExists(Parent p)
		{
			return _context.Parents.Any(e => 
            e.FirstName == p.FirstName && 
            e.LastName == p.LastName && 
            e.Phone == p.Phone &&
            e.Email == p.Email);
		}

		private int FindParentID(Parent p)
		{
			return _context.Parents.Where(e =>
			e.FirstName == p.FirstName &&
			e.LastName == p.LastName &&
			e.Phone == p.Phone &&
			e.Email == p.Email).First().Id;
		}
	}
}
