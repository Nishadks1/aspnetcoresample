using ASPNETCoreSample.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ASPNETCoreSample.MyControllers
{
    public class HelloController
    {
        private readonly ISampleService _sampleService;
        private readonly ILogger<HelloController> _logger;
        public HelloController(ISampleService sampleService, ILogger<HelloController> logger)
        {
            _sampleService = sampleService;
            _logger = logger;
        }

        public string GetList() =>
            Log("GetList", GetHtmlList);

        private string GetHtmlList() =>
            $"<ul>{GetListItems()}</ul>";

        private string GetListItems() =>
            string.Join("", _sampleService.GetSampleStrings().Select(s => $"<li>{s}</li>"));

        private string Log(string message, Func<string> action)
        {
            _logger.LogInformation($"{message} started");
            string result = action();
            _logger.LogInformation($"{message} completed");
            return result;
        }
    }
}
