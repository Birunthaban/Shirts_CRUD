using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase

    {


        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.getAllShirts());
        }

        [HttpGet("{id}")]
        [ShirtIdValidationFilter]
        public IActionResult GetParticularShirt(int id)
        {

            return Ok(ShirtRepository.getShirtById(id));
        }

        [HttpPut("{id}")]
        //[ShirtIdValidationFilter]
        [ShirtValidateUpdateShirtFilter]
        [ShirtHandleUpdateExceptionFilter]
        public IActionResult UpdateShirt(int id , Shirt shirt)
        {  
            
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ShirtIdValidationFilter]

        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.getShirtById(id);
            ShirtRepository.DeleteShirtById(id);
            return Ok(shirt);
        }

        [HttpPost]
        [ShirtValidateShirtCreateFilter]
        public IActionResult CreateShirt([FromBody] Shirt shirt)

        {

            ShirtRepository.createShirt(shirt);
            return CreatedAtAction(nameof(GetParticularShirt),
                new
                { id = shirt.Id },
                    shirt);
        }


    }
}



