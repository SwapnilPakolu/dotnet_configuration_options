using dotnet_configuration_options.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet_configuration_options.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsConfigurationController : ControllerBase
    {
        private readonly IOptions<OptionsConfigurationApiModel> options ;
        private readonly IOptionsSnapshot<OptionsConfigurationApiModel> optionsSnapshot;
        private readonly IOptionsMonitor<OptionsConfigurationApiModel> optionsMonitor;

        public OptionsConfigurationController(IOptions<OptionsConfigurationApiModel> options,IOptionsSnapshot<OptionsConfigurationApiModel> optionsSnapshot,IOptionsMonitor<OptionsConfigurationApiModel> optionsMonitor)
        {
            this.options = options;
            this.optionsSnapshot = optionsSnapshot;
            this.optionsMonitor = optionsMonitor;
        }

        // GET: api/<AboutController>
        [HttpGet("getIOptions")]
        public OptionsConfigurationApiModel GetIOptions()
        {

            return this.options.Value;
        }

        [HttpGet("getIOptionSnapshot")]
        public OptionsConfigurationApiModel GetIOptionSnapshot()
        {

            return this.optionsSnapshot.Value;
        }

        [HttpGet("getIOptionMonitor")]
        public OptionsConfigurationApiModel GetIOptionMonitor()
        {

            return this.optionsMonitor.CurrentValue;
        }
    }
}
