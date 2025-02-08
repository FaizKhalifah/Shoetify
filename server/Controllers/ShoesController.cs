using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Models.Entities;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoesController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IActionResult GetAllShoes()
        {
           var shoes =  _context.Shoes.ToList();
           return Ok(shoes);
        }

        [HttpPost]
        public IActionResult AddShoe(AddShoeDTO addShoeDTO)
        {
            try
            {
                
                var existingFactory = _context.Factories.Find(addShoeDTO.FactoryId);
                if (existingFactory == null)
                {
                    return BadRequest(new { message = "Factory ID not found" });
                }

                
                var ShoeEntity = new Shoe()
                {
                    Id = Guid.NewGuid(), 
                    Name = addShoeDTO.Name,
                    Description = addShoeDTO.Description,
                    Size = addShoeDTO.Size,
                    Brand = addShoeDTO.Brand,
                    FactoryId = addShoeDTO.FactoryId, 
                };

               
                _context.Shoes.Add(ShoeEntity);
                _context.SaveChanges();

               
                return CreatedAtAction(nameof(GetAllShoes), new { id = ShoeEntity.Id }, ShoeEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }

        }
    }
}
