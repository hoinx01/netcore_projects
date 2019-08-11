using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hinox.Mvc.Extensions;

namespace Hinox.Mvc.Controllers
{
    public class BaseMvcController : Controller
    {
        public bool FromMobile
        {
            get
            {
                return HttpContext.FromMobile();
            }
        }
    }
}
