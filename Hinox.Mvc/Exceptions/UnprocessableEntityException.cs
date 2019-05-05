using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Mvc.Exceptions
{
    public class UnprocessableEntityException : BaseCustomException
    {
        public UnprocessableEntityException(List<string> errors) : base(errors, System.Net.HttpStatusCode.UnprocessableEntity)
        {

        }
        public UnprocessableEntityException(string error) : base(new List<string>() { error }, System.Net.HttpStatusCode.UnprocessableEntity)
        {

        }
    }
}
