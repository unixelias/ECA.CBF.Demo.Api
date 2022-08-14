using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class TransferDbRepository : DbBaseRepositoryAsync, ITransferDbRepository
{
    public async Task<IEnumerable<TransferEntity>> ListAsync()
    {
        return await ListAsync<TransferEntity>(ScriptsSql.LIST_ALL_TEAMS);
    }
    
    public async Task<TransferEntity> GetAsync(int transferId)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@transfer_id", transferId, DbType.Int32);
        return await GetAsync<TransferEntity>(ScriptsSql.GET_TEAM, paramList);
    }
    
    public async Task<int> InsertAsync(TransferEntity transfer)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@transfer_type", transfer.Type, DbType.AnsiString);
        paramList.Add("@transfer_value", transfer.Value, DbType.Int64);
        paramList.Add("@transfer_observation", transfer.Comment, DbType.AnsiString);
        paramList.Add("@transfer_player_id", transfer.PlayerId, DbType.Int32);
        paramList.Add("@transfer_team_in_id", transfer.TeamIn, DbType.Int32);
        paramList.Add("@transfer_team_out_id", transfer.TeamOut, DbType.Int32);
        paramList.Add("@transfer_date_time", transfer.TransferDate, DbType.DateTime);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT_TEAM, paramList);
    }

    public async Task UpdateAsync(TransferEntity transfer)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@transfer_id", transfer.Id, DbType.Int32);
        paramList.Add("@transfer_type", transfer.Type, DbType.AnsiString);
        paramList.Add("@transfer_value", transfer.Value, DbType.Int64);
        paramList.Add("@transfer_observation", transfer.Comment, DbType.AnsiString);
        paramList.Add("@transfer_player_id", transfer.PlayerId, DbType.Int32);
        paramList.Add("@transfer_team_in_id", transfer.TeamIn, DbType.Int32);
        paramList.Add("@transfer_team_out_id", transfer.TeamOut, DbType.Int32);
        paramList.Add("@transfer_date_time", transfer.TransferDate, DbType.DateTime);

        await ExecuteAsync(ScriptsSql.UPDATE_TEAM, paramList);
    }
    
    public async Task DeleteAsync(int transferId)
    {
        
        var paramList = new DynamicParameters();
        paramList.Add("@transfer_id", transferId, DbType.Int32);
        await ExecuteAsync(ScriptsSql.DELETE_TEAM, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL_TEAMS = @"
            SELECT [transfer_id] AS Id
                  ,[transfer_type] AS Type
                  ,[transfer_value] AS Value
                  ,[transfer_observation] AS Comment
                  ,[transfer_player_id] AS PlayerId
                  ,[transfer_team_in_id] AS TeamIn
                  ,[transfer_team_out_id] AS TeamOut
                  ,[transfer_date_time] AS TransferDate
              FROM [dbo].[transfer]";

        public static readonly string GET_TEAM = LIST_ALL_TEAMS + @"
                WHERE [transfer_id] = @transfer_id";

        public static readonly string INSERT_TEAM = @"
            INSERT INTO [dbo].[transfer]
                       ([transfer_type]
                       ,[transfer_value]
                       ,[transfer_observation]
                       ,[transfer_player_id]
                       ,[transfer_team_in_id]
                       ,[transfer_team_out_id]
                       ,[transfer_date_time])
                 VALUES
                       (@transfer_type
                       ,@transfer_value
                       ,@transfer_observation
                       ,@transfer_player_id
                       ,@transfer_team_in_id
                       ,@transfer_team_out_id
                       ,@transfer_date_time)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE_TEAM = @"
            UPDATE [dbo].[transfer]
                SET [transfer_type] = @transfer_type
                    ,[transfer_value] = @transfer_value
                    ,[transfer_observation] = @transfer_observation
                    ,[transfer_player_id] = @transfer_player_id
                    ,[transfer_team_in_id] = @transfer_team_in_id
                    ,[transfer_team_out_id] = @transfer_team_out_id
                    ,[transfer_date_time] = @transfer_date_time
             WHERE [transfer_id] = @transfer_id";

        public static readonly string DELETE_TEAM = @"
            DELETE FROM [dbo].[transfer] WHERE [transfer_id] = @transfer_id";
    }
}