using Microsoft.Extensions.Configuration;

namespace ECA.CBF.Demo.Util;

public class DatabaseConfig
{
    #region Properties

    private const string CONNECTION_NAME = "CbfDb";
    public string CbfDb { get; private set; }

    #endregion Properties

    #region Constructor

    public DatabaseConfig(IConfiguration configuracao)
    {
        CbfDb = GetConfigKey(configuracao);
    }

    #endregion Constructor

    private static string GetConfigKey(IConfiguration configuracao)
    {
        return configuracao.GetConnectionString(CONNECTION_NAME);
    }
}