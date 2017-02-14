using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreSample.Services
{
    public class SampleService : ISampleService
    {
        private string[] _samples = { "one", "two", "three" };
        public IEnumerable<string> GetSampleStrings() => _samples;

    }
}
