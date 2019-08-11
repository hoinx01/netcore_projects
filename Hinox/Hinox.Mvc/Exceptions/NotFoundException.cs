using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Mvc.Exceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message = "Cannot find object") : base(new List<string>() { message}, System.Net.HttpStatusCode.NotFound)
        {

        }
    }
}
