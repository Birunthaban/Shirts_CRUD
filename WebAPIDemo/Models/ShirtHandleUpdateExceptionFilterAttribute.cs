using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIDemo.Models
{
    public class ShirtHandleUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId,out int shirtID))
            {
                if(!ShirtRepository.ShirtExists(shirtID))
                {
                    context.ModelState.AddModelError("ShirtId", "Shirt Doesn't Exist");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound

                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}
