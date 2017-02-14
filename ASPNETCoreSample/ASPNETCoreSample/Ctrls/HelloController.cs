using ASPNETCoreSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreSample.Ctrls
{
    public class HelloController
    {
        private readonly ISampleService _sampleService;
        public HelloController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public string GetList() =>
            $"<ul>{GetListItems()}</ul>";

        private string GetListItems() =>
            string.Join("", _sampleService.GetSampleStrings().Select(s => $"<li>{s}</li>"));
    }
}
