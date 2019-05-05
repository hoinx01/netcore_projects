using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hinox.Mvc.Exceptions
{
    public class AuthenticationException : BaseCustomException
    {
        public AuthenticationException(string message) : base(new List<string>() { message }, HttpStatusCode.Unauthorized)
        {

        }
    }
}
