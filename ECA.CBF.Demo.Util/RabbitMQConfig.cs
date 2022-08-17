using Microsoft.Extensions.Configuration;

namespace ECA.CBF.Demo.Util;

public class RabbitMQConfig
{
    #region Properties

    private const string CONNECTION_NAME = "RabbitMQ";
    public string Uri { get; private set; }

    #endregion Properties

    #region Constructor

    public RabbitMQConfig(IConfiguration configuracao)
    {
        Uri = GetConfigKey(configuracao);
    }

    #endregion Constructor

    private static string GetConfigKey(IConfiguration configuracao)
    {
        return configuracao.GetConnectionString(CONNECTION_NAME);
    }
}