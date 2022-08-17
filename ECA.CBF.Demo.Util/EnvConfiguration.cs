using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ECA.CBF.Demo.Util;

public class EnvConfiguration
{
    #region Properties

    private static EnvConfiguration _configuration;

    public DatabaseConfig DatabaseConfig { get; private set; }
    public RabbitMQConfig RabbitMQConfig { get; private set; }

    #endregion Properties

    #region Constructor

    private EnvConfiguration(IConfiguration config)
    {
        DatabaseConfig = new DatabaseConfig(config);
        RabbitMQConfig = new RabbitMQConfig(config);
    }

    #endregion Constructor

    #region Methods

    public static EnvConfiguration Get()
    {
        if (_configuration is null)
        {
            _configuration = GetConfigsFromEnv();
        }
        return _configuration;
    }

    private static EnvConfiguration GetConfigsFromEnv()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return new EnvConfiguration(builder.Build());
    }

    #endregion Methods
}