﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mongo.ValueObjects
{
    public class MdImage
    {
        public string CdnPath { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
