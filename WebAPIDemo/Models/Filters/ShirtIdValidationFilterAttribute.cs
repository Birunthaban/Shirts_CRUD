using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Data;

namespace WebAPIDemo.Models.Filters
{
    public class ShirtIdValidationFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;
        
        public ShirtIdValidationFilterAttribute(ApplicationDbContext db)
        {
            this.db = db;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var ShirtId = context.ActionArguments["id"] as int?;
            if (ShirtId.HasValue)

            {
                if (ShirtId.Value < 0)
                {
                    context.ModelState.AddModelError("Id", "Invalid Shirt Id");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest

                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else  
                {
                    var shirt = db.Shirts.Find(ShirtId);
                    if (shirt == null) 
                    {
                        context.ModelState.AddModelError("Id", "Shirt Doesn't exist");
                        context.Result = new BadRequestObjectResult(context.ModelState);
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound

                        };
                        context.Result = new BadRequestObjectResult(problemDetails);
                    }
                    else
                    {
                        context.HttpContext.Items["shirt"] = shirt;
                    }

                    


                }
            }
        }
    }
}
