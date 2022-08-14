using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class PlayerDbRepository : DbBaseRepositoryAsync, IPlayerDbRepository
{
    public async Task<IEnumerable<PlayerEntity>> ListPlayerAsync()
    {
        return await ListAsync<PlayerEntity>(ScriptsSql.LIST_ALL_PLAYERS);
    }
    
    public async Task<PlayerEntity> GetPlayerAsync(int playerId)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@player_id", playerId, DbType.Int32);
        return await GetAsync<PlayerEntity>(ScriptsSql.GET_PLAYER, paramList);
    }
    
    public async Task<int> InsertPlayerAsync(PlayerEntity player)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@player_name", player.Name, DbType.AnsiString);
        paramList.Add("@player_dt_birth", player.BirthDate, DbType.DateTime);
        paramList.Add("@player_country", player.Country, DbType.AnsiString);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT_PLAYER, paramList);
    }

    public async Task UpdatePlayerAsync(PlayerEntity player)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@player_id", player.Id, DbType.Int32);
        paramList.Add("@player_name", player.Name, DbType.AnsiString);
        paramList.Add("@player_dt_birth", player.BirthDate, DbType.DateTime);
        paramList.Add("@player_country", player.Country, DbType.AnsiString);

        await ExecuteAsync(ScriptsSql.UPDATE_PLAYER, paramList);
    }
    
    public async Task DeletePlayerAsync(int playerId)
    {
        
        var paramList = new DynamicParameters();
        paramList.Add("@player_id", playerId, DbType.Int32);
        await ExecuteAsync(ScriptsSql.DELETE_PLAYER, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL_PLAYERS = @"
                SELECT [player_id] as Id
                  ,[player_name] as Name
                  ,[player_dt_birth] as BirthDate
                  ,[player_country] as Country
              FROM [dbo].[player]";

        public static readonly string GET_PLAYER = LIST_ALL_PLAYERS + @"
                WHERE [player_id]= @player_id";

        public static readonly string INSERT_PLAYER = @"
            INSERT INTO [dbo].[player]
                       ([player_name]
                       ,[player_dt_birth]
                       ,[player_country])
                 VALUES
                       (@player_name
                       ,@player_dt_birth
                       ,@player_country)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE_PLAYER = @"
            UPDATE [dbo].[player]
               SET [player_name] = @player_name
                  ,[player_dt_birth] = @player_dt_birth
                  ,[player_country] = @player_country
             WHERE [player_id] = @player_id";
        
        public static readonly string DELETE_PLAYER = @"
            DELETE FROM [dbo].[player] WHERE [player_id] = @player_id";
    }
}