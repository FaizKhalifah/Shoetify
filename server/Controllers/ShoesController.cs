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

        [HttpPut("{id}")]
        public IActionResult updateShoe(Guid id, [FromBody] UpdateShoeDTO updateShoeDTO)
        {
            try
            {
                var shoe = _context.Shoes.Find(id);
                if (shoe == null)
                {
                    return NotFound(new { message = "shoe not found" });
                }
                shoe.Name = updateShoeDTO.Name;
                shoe.Description = updateShoeDTO.Description;
                shoe.Size = updateShoeDTO.Size;
                shoe.Brand = updateShoeDTO.Brand;
                shoe.FactoryId = updateShoeDTO.FactoryId;

                _context.SaveChanges();
                return Ok(shoe.Name + "updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteShoe(Guid id)
        {
            try
            {
                var shoe = _context.Shoes.Find(id);
                if (shoe == null)
                {
                    return NotFound(new { message = "shoe not found" });
                }
                _context.Shoes.Remove(shoe);
                _context.SaveChanges();
                return Ok(new { message = "Shoe deleted successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}
