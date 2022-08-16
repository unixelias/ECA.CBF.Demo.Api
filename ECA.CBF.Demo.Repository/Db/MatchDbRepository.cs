using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Entities.Exceptions;
using ECA.CBF.Demo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class MatchDbRepository : DbBaseRepositoryAsync, IMatchDbRepository
{
    public async Task<IEnumerable<MatchEntity>> ListAsync()
    {
        return await ListAsync<MatchEntity>(ScriptsSql.LIST_ALL);
    }

    public async Task<DateTime?> GetStartAsync(int tournmentId, int matchId)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", tournmentId, DbType.Int32);
        paramList.Add("@match_id", matchId, DbType.Int32);
        
        var match = await GetAsync<MatchEntity>(ScriptsSql.GET_WITH_TORUNMENT, paramList);

        if (match is null)
        {
            throw new ResourceNotFoundException("Match or Tournment not found!");
        }

        return match?.DateStart;
    }

    public async Task<int> SetStartAsync(int tournmentId, int matchId, DateTime? dateStart)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", tournmentId, DbType.Int32);
        paramList.Add("@match_id", matchId, DbType.Int32);
        paramList.Add("@match_dt_start", dateStart, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE_START_DATE, paramList);
    }
    public async Task<int> SetEndAsync(int tournmentId, int matchId, DateTime? dateEnd)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", tournmentId, DbType.Int32);
        paramList.Add("@match_id", matchId, DbType.Int32);
        paramList.Add("@match_dt_end", dateEnd, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE_END_DATE, paramList);
    }
    public async Task<int> SetStartBreakAsync(int tournmentId, int matchId, DateTime? dateStartBreak)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", tournmentId, DbType.Int32);
        paramList.Add("@match_id", matchId, DbType.Int32);
        paramList.Add("@match_dt_start_break", dateStartBreak, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE_START_BRAKE_DATE, paramList);
    }

    public async Task<int> SetEndBreakAsync(int tournmentId, int matchId, DateTime? dateEndBreak)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", tournmentId, DbType.Int32);
        paramList.Add("@match_id", matchId, DbType.Int32);
        paramList.Add("@match_dt_end_break", dateEndBreak, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE_END_BRAKE_DATE, paramList);
    }

    public async Task<MatchEntity> GetAsync(int id)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@match_id", id, DbType.Int32);
        return await GetAsync<MatchEntity>(ScriptsSql.GET, paramList);
    }
    
    public async Task<int> InsertAsync(MatchBaseEntity entity)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@match_stadium", entity.Stadium, DbType.AnsiString);
        paramList.Add("@match_dt_provided", entity.DateProvided, DbType.DateTime);
        paramList.Add("@match_dt_start", entity.DateStart, DbType.DateTime);
        paramList.Add("@match_dt_start_break", entity.DateStartBreak, DbType.DateTime);
        paramList.Add("@match_dt_end_break", entity.DateStopBreak, DbType.DateTime);
        paramList.Add("@match_dt_end", entity.DateEnd, DbType.DateTime);
        paramList.Add("@match_tournament_id", entity.TournmentId, DbType.Int32);
        paramList.Add("@match_team_host", entity.TeamHostId, DbType.Int32);
        paramList.Add("@match_team_guest", entity.TeamGuestId, DbType.Int32);
        paramList.Add("@match_referee", entity.RefereeId, DbType.Int32);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT, paramList);
    }

    public async Task UpdateAsync(MatchBaseEntity entity)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@match_id", entity.Id, DbType.Int32);
        paramList.Add("@match_stadium", entity.Stadium, DbType.AnsiString);
        paramList.Add("@match_dt_provided", entity.DateProvided, DbType.DateTime);
        paramList.Add("@match_dt_start", entity.DateStart, DbType.DateTime);
        paramList.Add("@match_dt_start_break", entity.DateStartBreak, DbType.DateTime);
        paramList.Add("@match_dt_end_break", entity.DateStopBreak, DbType.DateTime);
        paramList.Add("@match_dt_end", entity.DateEnd, DbType.DateTime);
        paramList.Add("@match_tournament_id", entity.TournmentId, DbType.Int32);
        paramList.Add("@match_team_host", entity.TeamHostId, DbType.Int32);
        paramList.Add("@match_team_guest", entity.TeamGuestId, DbType.Int32);
        paramList.Add("@match_referee", entity.RefereeId, DbType.Int32);

        await ExecuteAsync(ScriptsSql.UPDATE, paramList);
    }
    
    public async Task DeleteAsync(int id)
    {
        
        var paramList = new DynamicParameters();
        paramList.Add("@match_id", id, DbType.Int32);
        await ExecuteAsync(ScriptsSql.DELETE, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL = @"
            SELECT [m].[match_id] AS Id
              ,[m].[match_stadium] AS Stadium
              ,[m].[match_dt_provided] AS DateProvided
              ,[m].[match_dt_start] AS DateStart
              ,[m].[match_dt_start_break] AS DateStartBreak
              ,[m].[match_dt_end_break] AS DateStopBreak
              ,[m].[match_dt_end] AS DateEnd
              ,[m].[match_tournament_id] AS TournmentId
	          ,[t].[tournament_name] AS Tournment
              ,[m].[match_team_host] AS TeamHostId
	          ,[th].[team_name] AS TeamHost
	          ,(SELECT COUNT(*) FROM [dbo].[goal] [gg] WHERE [gg].[goal_match_id] = [m].[match_id] AND [gg].[goal_team_id] = [m].[match_team_host]) AS TeamHostGoals
              ,[m].[match_team_guest] AS TeamGuestId
	          ,[tg].[team_name] AS TeamGuest
	          ,(SELECT COUNT(*) FROM [dbo].[goal] [gg] WHERE [gg].[goal_match_id] = [m].[match_id] AND [gg].[goal_team_id] = [m].[match_team_guest]) AS TeamGuestGoals
              ,[m].[match_referee] AS RefereeId
	          ,[r].[player_name] AS RefereeName
          FROM [dbo].[match] [m]
          LEFT JOIN [dbo].[player] [r] ON [r].[player_id] = [m].[match_referee]
          LEFT JOIN [dbo].[tournament] [t] ON [t].[tournament_id] = [m].[match_tournament_id]
          LEFT JOIN [dbo].[team] [th] ON [th].[team_id] = [m].[match_team_host]
          LEFT JOIN [dbo].[team] [tg] ON [tg].[team_id] = [m].[match_team_guest]";

        public static readonly string GET = LIST_ALL + @"
                WHERE [m].[match_id] = @match_id";
        
        public static readonly string GET_WITH_TORUNMENT = LIST_ALL + @"
                WHERE [m].[match_id] = @match_id AND  [m].[match_tournament_id] = @tournament_id";

        public static readonly string INSERT = @"
            INSERT INTO [dbo].[match]
                       ([match_stadium]
                       ,[match_dt_provided]
                       ,[match_dt_start]
                       ,[match_dt_start_break]
                       ,[match_dt_end_break]
                       ,[match_dt_end]
                       ,[match_tournament_id]
                       ,[match_team_host]
                       ,[match_team_guest]
                       ,[match_referee])
                 VALUES
                       (@match_stadium
                       ,@match_dt_provided
                       ,@match_dt_start
                       ,@match_dt_start_break
                       ,@match_dt_end_break
                       ,@match_dt_end
                       ,@match_tournament_id
                       ,@match_team_host
                       ,@match_team_guest
                       ,@match_referee)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE = @"
            UPDATE [dbo].[match]
               SET [match_stadium] = @match_stadium
                  ,[match_dt_provided] = @match_dt_provided
                  ,[match_dt_start] = @match_dt_start
                  ,[match_dt_start_break] = @match_dt_start_break
                  ,[match_dt_end_break] = @match_dt_end_break
                  ,[match_dt_end] = @match_dt_end
                  ,[match_tournament_id] = @match_tournament_id
                  ,[match_team_host] = @match_team_host
                  ,[match_team_guest] = @match_team_guest
                  ,[match_referee] = @match_referee
             WHERE [match_id] = @match_id";

        public static readonly string UPDATE_START_DATE = @"
            UPDATE [dbo].[match]
               SET [match_dt_start] = @match_dt_start
            WHERE [match_id] = @match_id AND  [match_tournament_id] = @tournament_id";

        public static readonly string UPDATE_END_DATE = @"
            UPDATE [dbo].[match]
               SET [match_dt_end] = @match_dt_end
            WHERE [match_id] = @match_id AND  [match_tournament_id] = @tournament_id";
        
        public static readonly string UPDATE_START_BRAKE_DATE = @"
            UPDATE [dbo].[match]
               SET [match_dt_start_break] = @match_dt_start_break
            WHERE [match_id] = @match_id AND  [match_tournament_id] = @tournament_id";

        public static readonly string UPDATE_END_BRAKE_DATE = @"
            UPDATE [dbo].[match]
               SET [match_dt_end_break] = @match_dt_end_break
            WHERE [match_id] = @match_id AND  [match_tournament_id] = @tournament_id";

        public static readonly string DELETE = @"
            DELETE FROM [dbo].[match] WHERE [match_id] = @match_id";
    }
}