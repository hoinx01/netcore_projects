﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hinox.Mvc.Configs
{
    public class HostedServiceConfig
    {
        public string Name { get; set; }
        public string Assembly { get; set; }
        public bool Enable { get; set; }
    }
}
