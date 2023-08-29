﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using System.Reflection;
using WebAPIDemo.Data;
using WebAPIDemo.Models.CustomValidations;

namespace WebAPIDemo.Models.Filters
{
    public class ShirtValidateShirtCreateFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext db;
        public ShirtValidateShirtCreateFilterAttribute(ApplicationDbContext db)
        {
            this.db =db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var shirt = context.ActionArguments["shirt"] as Shirt;
            if (shirt == null)
            {
                context.ModelState.AddModelError("shirt", "shirt object is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingShirt = db.Shirts.FirstOrDefault(x =>
          !string.IsNullOrWhiteSpace(shirt.Brand) &&
          !string.IsNullOrWhiteSpace(x.Brand) &&
          x.Brand.ToLower()==shirt.Brand.ToLower() &&
          !string.IsNullOrWhiteSpace(shirt.Gender) &&
          !string.IsNullOrWhiteSpace(x.Gender) &&
          x.Gender.ToLower() == shirt.Gender.ToLower() &&
          !string.IsNullOrWhiteSpace(shirt.Color) &&
          !string.IsNullOrWhiteSpace(x.Color) &&
          x.Color.ToLower() == shirt.Color.ToLower() &&
          shirt.Size.HasValue && x.Size.HasValue && shirt.Size.Value == x.Size.Value); ;
                if (existingShirt != null)
                {

                    context.ModelState.AddModelError("shirt", "shirt already exists");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);

                }
            }
        }
    }
}