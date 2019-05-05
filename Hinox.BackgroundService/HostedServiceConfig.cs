using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.BackgroundService
{
    public class HostedServiceConfig
    {
        public string Name { get; set; }
        public string Assembly { get; set; }
        public bool Enable { get; set; }
    }
}
