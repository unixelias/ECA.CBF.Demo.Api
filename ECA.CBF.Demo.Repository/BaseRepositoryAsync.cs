using Dapper;
using ECA.CBF.Demo.Entities.Exceptions;
using ECA.CBF.Demo.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository;

public abstract class DbBaseRepositoryAsync
{
    private readonly string _connectionString;

    public DbBaseRepositoryAsync()
    {
        _connectionString = EnvConfiguration.Get().DatabaseConfig.CbfDb;
    }

    protected async Task<IEnumerable<TObject>> ListAsync<TObject>(string command, object param = null, int? commandTimeout = null)
    {
        using var conexao = new SqlConnection(_connectionString);
        try
        {
            return await conexao.QueryAsync<TObject>(command, param, commandTimeout: commandTimeout);
        }
        catch (Exception ex)
        {
            throw new InternalErrorException(ex.Message, ex);
        }
    }

    protected async Task<TObject> GetAsync<TObject>(string sql, object param, int? commandTimeout = null)
    {
        using var conexao = new SqlConnection(_connectionString);
        try
        {
            return await conexao.QueryFirstOrDefaultAsync<TObject>(sql, param, commandTimeout: commandTimeout);
        }
        catch (Exception ex)
        {
            throw new InternalErrorException(ex.Message, ex);
        }
    }

    protected async Task ExecuteAsync(string sql, object param, int? commandTimeout = null)
    {
        using var conexao = new SqlConnection(_connectionString);
        try
        {
            await conexao.ExecuteAsync(sql, param, commandTimeout: commandTimeout);
        }
        catch (Exception ex)
        {
            throw new InternalErrorException(ex.Message, ex);
        }
    }
}