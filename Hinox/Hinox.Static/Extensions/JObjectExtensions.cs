using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Static.Extensions
{
    public static class JObjectExtensions
    {
        public static bool HasToken(this JObject obj, string path)
        {
            var token = obj.SelectToken(path);
            if (token == null)
                return false;
            return true;
        }
    }
}
