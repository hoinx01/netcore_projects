using Hinox.Mvc.Exceptions;
using Hinox.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;
            var errors = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .Select(e => string.Format("{0}: {1}", e.Key, e.Value.Errors.First().ErrorMessage))
                .ToList();

            throw new UnprocessableEntityException(errors);
        }
    }
}
