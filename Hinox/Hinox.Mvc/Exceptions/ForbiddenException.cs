using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Mvc.Exceptions
{
    public class ForbiddenException : BaseCustomException
    {
        public ForbiddenException(string message) : base(new List<string>() { message}, System.Net.HttpStatusCode.Forbidden)
        {

        }
    }
}
