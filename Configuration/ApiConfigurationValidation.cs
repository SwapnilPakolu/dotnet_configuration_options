using dotnet_configuration_options.Model;
using Microsoft.Extensions.Options;

namespace dotnet_configuration_options.Configuration
{
    public class ApiConfigurationValidation : IValidateOptions<ApiConfiguration>
    {
        public ValidateOptionsResult Validate(string name, ApiConfiguration options)
        {
            if(options.ApiVersion== null) {
                //throw new ArgumentNullException(nameof(options.ApiVersion));
                return ValidateOptionsResult.Fail("ApiVersion is Required");
            }
            return ValidateOptionsResult.Success;
        }
    }
}
