﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hinox.Mvc.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
        public object AdditionalData { get; set; }

        public ErrorModel(HttpStatusCode statusCode, List<string> messages)
        {
            StatusCode = (int)statusCode;
            Messages = messages;
        }
        public ErrorModel()
        {

        }
    }
}
