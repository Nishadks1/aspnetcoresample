﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreSample.Services
{
    public interface ISampleService
    {
        IEnumerable<string> GetSampleStrings();
    }
}
