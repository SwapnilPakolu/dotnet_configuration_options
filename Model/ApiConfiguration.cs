namespace dotnet_configuration_options.Model
{
    public class ApiConfiguration
    {
        public const string AboutApi = "AboutApi";
        public const string CatlogApi = "CatlogApi";
        public bool Enabled { get; set; }
        public string DefaultMessage { get; set; }
        public bool AuthenticationRequired { get; set; }
        public string ApiVersion { get; set; }
    }
}
