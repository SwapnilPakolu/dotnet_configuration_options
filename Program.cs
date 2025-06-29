using dotnet_configuration_options.Configuration;
using dotnet_configuration_options.Model;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

namespace dotnet_configuration_options
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect("Endpoint=https://git-demo-app-configuration.azconfig.io;Id=d1SE;Secret=DO2PtIItnY7DBSCS72Zgi7iebpmxoABM2HZZCdFujy43iayZrAMzJQQJ99BFACBsN54hStKcAAABAZAC3j2H");
                options.UseFeatureFlags(); // Correct method name
                options.ConfigureRefresh(refreshOptions =>
                   {
                        refreshOptions.Register("FeatureManagement:FeatureFlags", refreshAll: true)
                          .SetCacheExpiration(TimeSpan.FromSeconds(10)); // Refresh every 10 seconds
                   });

            });

            
            /*
            //add configuration without using validate
            builder.Services.Configure<OptionsConfigurationApiModel>(builder.Configuration.GetSection("aboutapi"));

            //using addoptions.bind to use validatedataannotations function
            builder.Services.AddOptions<OptionsConfigurationApiModel>().Bind(builder.Configuration.GetSection("aboutapi")).ValidateDataAnnotations().ValidateOnStart();

            //using addoptions.validate(delegate) using anonymouse function passed to validate function
            builder.Services.AddOptions<OptionsConfigurationApiModel>().Bind(builder.Configuration.GetSection("aboutapi")).Validate(c =>
            {
                if (!c.AuthenticationRequired)
                {
                    //check if user is authorized if not return false;
                }
                return true;
            }, "user must be authenticated").ValidateOnStart();
            */

            // Enable Feature Management in Dependency Injection
            builder.Services.AddFeatureManagement();
            
            //using AddOptions to bind to use ValidateOnStart function Validate
            builder.Services.AddOptions<OptionsConfigurationApiModel>().Bind(builder.Configuration.GetSection("AboutApi")).ValidateOnStart();
            // using IValidateOptions instead of ValidateDataAnnotations or Validate
            // to configure Validator(AboutApiConfigurationValidation) to validate configuration
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<OptionsConfigurationApiModel>, AboutApiConfigurationValidation>());

            // we can use IAboutApiConfiguration for dependency injection instead of IOptions or IOptionsMonitor or IOptionsSnapshot
            builder.Services.AddSingleton<IOptionsConfigurationApiModel>(sp => { return sp.GetRequiredService<IOptions<OptionsConfigurationApiModel>>().Value; }); 

            //configuring named options
            builder.Services.Configure<ApiConfiguration>(ApiConfiguration.AboutApi, builder.Configuration.GetSection(ApiConfiguration.AboutApi));
            builder.Services.Configure<ApiConfiguration>(ApiConfiguration.CatlogApi, builder.Configuration.GetSection(ApiConfiguration.CatlogApi));


            var app = builder.Build();

            var x = builder.Configuration.GetValue<string>("HomeApi:stringKey");

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
