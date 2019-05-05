using Hinox.Mvc.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Hinox.Mvc.Controllers
{

    public abstract class BaseRestController : ControllerBase
    {
        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in ModelState.Values)
                    foreach (var error in modelState.Errors)
                        errors.Add(error.ErrorMessage);
                throw new UnprocessableEntityException(errors);
            }
        }
        public override bool TryValidateModel(object model)
        {
            return base.TryValidateModel(model);
        }
        public override bool TryValidateModel(object model, string prefix)
        {
            return base.TryValidateModel(model, prefix);
        }
        protected string GetTextBody()
        {
            string text = (string)HttpContext.Items["BodyText"];
            return text;
        }
        protected JObject GetJBody()
        {
            var textBody = GetTextBody();
            if (string.IsNullOrWhiteSpace(textBody))
                return null;

            var jsonTree = JObject.Parse(textBody);
            return jsonTree;
        }
        
    }
}
