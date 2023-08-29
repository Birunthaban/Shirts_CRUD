using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIDemo.Data;
using WebAPIDemo.Models;
using WebAPIDemo.Models.CustomValidations;
using WebAPIDemo.Models.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
        
    {
        private readonly ApplicationDbContext db;
        public ShirtsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(db.Shirts.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ShirtIdValidationFilterAttribute))]
        public IActionResult GetParticularShirt(int id)
        {

            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(ShirtIdValidationFilterAttribute))]
        [ShirtValidateUpdateShirtFilter]
        [ShirtHandleUpdateExceptionFilter]
        public IActionResult UpdateShirt(int id , Shirt shirt)
        {  
            
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(ShirtIdValidationFilterAttribute))]

        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.getShirtById(id);
            ShirtRepository.DeleteShirtById(id);
            return Ok(shirt);
        }

        [HttpPost]
        [TypeFilter(typeof(ShirtValidateShirtCreateFilterAttribute))]
        public IActionResult CreateShirt([FromBody] Shirt shirt)

        {
            this.db.Shirts.Add(shirt);
            this.db.SaveChanges();
            return CreatedAtAction(nameof(GetParticularShirt),
                new
                { id = shirt.Id },
                    shirt);
        }


    }
}



