using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_configuration_options.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicConfigurationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<BasicConfigurationController> _logger;
        private readonly IConfiguration _configuration;

        public BasicConfigurationController(ILogger<BasicConfigurationController> logger,IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetConfiguration")]
        public HomeApi Get()
        {
            var x = _configuration.GetSection("HomeApi");
            Console.WriteLine(x.GetValue<string>("stringKey"));
            Console.WriteLine(x.GetValue<bool>("boolKey"));
            Console.WriteLine(x.GetValue<int>("intKey"));
            var y = x["intKey"];

            HomeApi homeApi = new HomeApi();

            _configuration.Bind("HomeApi",homeApi);//HomeApi class properties must have setter

            return homeApi;
        }

        public class HomeApi
        {

            public string stringKey { get; set; }
            public bool boolKey { get; set; }

            public int intKey { get; set; }

        }
    }
}