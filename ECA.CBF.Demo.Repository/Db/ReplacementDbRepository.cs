using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class ReplacementDbRepository : DbBaseRepositoryAsync, IReplacementDbRepository
{
    public async Task<IEnumerable<ReplacementEntity>> ListAsync(int id)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@replacement_match_id", id, DbType.Int32);
        return await ListAsync<ReplacementEntity>(ScriptsSql.GET_BY_MATCH, paramList);
    }
    
    public async Task<int> InsertAsync(ReplacementEntity entity)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@replacement_match_id", entity.MatchId, DbType.Int32);
        paramList.Add("@replacement_player_in_id", entity.PlayerInId, DbType.AnsiString);
        paramList.Add("@replacement_player_out_id", entity.PlayerOutId, DbType.Int32);
        paramList.Add("@replacement_team_id", entity.TeamId, DbType.Int32);
        paramList.Add("@replacement_date_time", entity.Date, DbType.DateTime);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT, paramList);
    }

    public async Task<int> UpdateAsync(ReplacementEntity entity)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@replacement_id", entity.Id, DbType.Int32);
        paramList.Add("@replacement_match_id", entity.MatchId, DbType.Int32);
        paramList.Add("@replacement_player_in_id", entity.PlayerInId, DbType.AnsiString);
        paramList.Add("@replacement_player_out_id", entity.PlayerOutId, DbType.Int32);
        paramList.Add("@replacement_team_id", entity.TeamId, DbType.Int32);
        paramList.Add("@replacement_date_time", entity.Date, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE, paramList);
    }
    
    public async Task<int> DeleteAsync(int id)
    {        
        var paramList = new DynamicParameters();
        paramList.Add("@replacement_id", id, DbType.Int32);
        return await ExecuteAsync(ScriptsSql.DELETE, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL = @"
            SELECT [r].[replacement_id] AS Id
                  ,[r].[replacement_match_id] AS MatchId
	              ,[pi].[player_name] AS PlayerInName
                  ,[r].[replacement_player_in_id] AS PlayerInId
                  ,[po].[player_name] AS PlayerOutName
	              ,[r].[replacement_player_out_id] AS PlayerOutId
                  ,[r].[replacement_team_id] AS TeamId
	              ,[t].[team_name] AS TeamName
                  ,[r].[replacement_date_time] AS Date
              FROM [dbo].[replacement] [r]
              INNER JOIN [dbo].[team] [t] ON [r].[replacement_team_id] = [t].[team_id]
              INNER JOIN [dbo].[player] [pi] ON [r].[replacement_player_in_id] = [pi].[player_id]
              INNER JOIN [dbo].[player] [po] ON [r].[replacement_player_out_id] = [po].[player_id]";

        public static readonly string GET_BY_MATCH = LIST_ALL + @"
                WHERE [r].[replacement_match_id] = @replacement_match_id";

        public static readonly string INSERT = @"
            INSERT INTO [dbo].[replacement]
                       ([replacement_match_id]
                       ,[replacement_player_in_id]
                       ,[replacement_player_out_id]
                       ,[replacement_team_id]
                       ,[replacement_date_time])
                 VALUES
                       (@replacement_match_id
                       ,@replacement_player_in_id
                       ,@replacement_player_out_id
                       ,@replacement_team_id
                       ,@replacement_date_time)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE = @"
            UPDATE [dbo].[replacement]
               SET [replacement_match_id] = @replacement_match_id
                  ,[replacement_player_in_id] = @replacement_player_in_id
                  ,[replacement_player_out_id] = @replacement_player_out_id
                  ,[replacement_team_id] = @replacement_team_id
                  ,[replacement_date_time] = @replacement_date_time
             WHERE [replacement_id] = @replacement_id";

        public static readonly string DELETE = @"
            DELETE FROM [dbo].[replacement] WHERE [replacement_id] = @replacement_id";
    }
}