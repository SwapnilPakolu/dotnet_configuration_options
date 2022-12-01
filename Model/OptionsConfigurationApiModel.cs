using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace dotnet_configuration_options.Model
{
    public interface IOptionsConfigurationApiModel { }
    public class OptionsConfigurationApiModel : IOptionsConfigurationApiModel
    {
        public bool Enabled { get; set; }
        public string DefaultMessage { get; set; }
        public bool AuthenticationRequired { get; set; }
        [Required]
        public string ApiVersion { get; set; }

    }
}
