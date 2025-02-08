using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var factories = _context.Factories.ToList();
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

    }
}
