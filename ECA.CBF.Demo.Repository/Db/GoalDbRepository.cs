using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class GoalDbRepository : DbBaseRepositoryAsync, IGoalDbRepository
{
    public async Task<IEnumerable<GoalEntity>> ListAsync(int id)
    {

        var paramList = new DynamicParameters();
        paramList.Add("@goal_match_id", id, DbType.Int32);
        return await ListAsync<GoalEntity>(ScriptsSql.GET_BY_MATCH, paramList);
    }
    
    public async Task<int> InsertAsync(GoalEntity entity)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@goal_match_id", entity.MatchId, DbType.Int32);
        paramList.Add("@goal_player_id", entity.PlayerId, DbType.Int32);
        paramList.Add("@goal_team_id", entity.TeamId, DbType.Int32);
        paramList.Add("@goal_date_time", entity.Date, DbType.DateTime);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT, paramList);
    }

    public async Task<int> UpdateAsync(GoalEntity entity)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@goal_id", entity.Id, DbType.Int32);
        paramList.Add("@goal_match_id", entity.MatchId, DbType.Int32);
        paramList.Add("@goal_player_id", entity.PlayerId, DbType.Int32);
        paramList.Add("@goal_team_id", entity.TeamId, DbType.Int32);
        paramList.Add("@goal_date_time", entity.Date, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE, paramList);
    }
    
    public async Task<int> DeleteAsync(int id)
    {        
        var paramList = new DynamicParameters();
        paramList.Add("@goal_id", id, DbType.Int32);
        return await ExecuteAsync(ScriptsSql.DELETE, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL = @"
            SELECT [g].[goal_id] AS Id
                  ,[g].[goal_match_id] AS MatchId
                  ,[g].[goal_player_id] AS PlayerId
	              ,[p].[player_name] AS Player
                  ,[g].[goal_team_id] AS TeamId
	              ,[t].[team_name] As Team
                  ,[g].[goal_date_time] As Date
              FROM [dbo].[goal] [g]
              INNER JOIN [dbo].[team] [t] ON [g].[goal_team_id] = [t].[team_id]
              INNER JOIN [dbo].[player] [p] ON [g].[goal_player_id] = [p].[player_id]";

        public static readonly string GET_BY_MATCH = LIST_ALL + @"
                WHERE [g].[goal_match_id] = @goal_match_id";

        public static readonly string INSERT = @"
            INSERT INTO [dbo].[goal]
                       ([goal_match_id]
                       ,[goal_player_id]
                       ,[goal_team_id]
                       ,[goal_date_time])
                 VALUES
                       (@goal_match_id
                       ,@goal_player_id
                       ,@goal_team_id
                       ,@goal_date_time)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE = @"
            UPDATE [dbo].[goal]
               SET [goal_match_id] = @goal_match_id
                  ,[goal_player_id] = @goal_player_id
                  ,[goal_team_id] = @goal_team_id
                  ,[goal_date_time] = @goal_date_time
             WHERE [goal_id] = @goal_id";

        public static readonly string DELETE = @"
            DELETE FROM [dbo].[goal] WHERE [goal_id] = @goal_id";
    }
}