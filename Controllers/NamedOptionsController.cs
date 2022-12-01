using dotnet_configuration_options.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_configuration_options.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamedOptionsController : ControllerBase
    {
        private readonly IOptionsMonitor<ApiConfiguration> optionsMonitor;

        public NamedOptionsController(IOptionsMonitor<ApiConfiguration> optionsMonitor)
        {
            this.optionsMonitor = optionsMonitor;
        }

        // GET: api/<NamedOptionsController>
        [HttpGet("AboutApiConfiguration")]
        public ApiConfiguration GetAboutApi()
        {
            return this.optionsMonitor.Get(ApiConfiguration.AboutApi);
        }

        [HttpGet("CatlogApiConfiguration")]
        public ApiConfiguration GetCatlogApi()
        {
            return this.optionsMonitor.Get(ApiConfiguration.CatlogApi);
        }
    }
}
