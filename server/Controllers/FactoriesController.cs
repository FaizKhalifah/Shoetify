using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Models.Entities;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FactoriesController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IActionResult GetAllFactories()
        {
            var factories = _context.Factories
            .Include(f => f.Shoes) 
            .ToList();

            return Ok(factories);
        }

        [HttpPost]
        public IActionResult AddFactory(AddFactoryDTO addFactoryDTO)
        {
            try
            {
                var newFactory = new Factory()
                {
                    Id = Guid.NewGuid(),
                    Name = addFactoryDTO.Name,
                    Location = addFactoryDTO.Location
                };

                _context.Factories.Add(newFactory);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetAllFactories), new { id = newFactory.Id }, newFactory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult updateFactory(Guid id, [FromBody] UpdateFactoryDTO updateFactoryDTO)
        {
            try
            {
                var factory = _context.Factories.Find(id);
                if (factory == null)
                {
                    return StatusCode(400, new { message = $"Factory not existed" });
                }
                factory.Name = updateFactoryDTO.Name;
                factory.Location = updateFactoryDTO.Location;

                _context.SaveChanges();
                return Ok(factory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteFactory(Guid id)
        {
            try
            {
                var factory = _context.Factories.Include(f => f.Shoes).FirstOrDefault(f => f.Id == id);
                if (factory == null)
                {
                    return NotFound(new {message="factory not found"} );
                }
                _context.Shoes.RemoveRange(factory.Shoes);
                _context.Factories.Remove(factory);
                _context.SaveChanges();

                return Ok("Factory " + factory.Name + " deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

    }
}
