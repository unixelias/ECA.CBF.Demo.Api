using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class TeamBdRepository : DbBaseRepositoryAsync, ITeamDbRepository
{
    public async Task<IEnumerable<TeamEntity>> ListTeamsAsync()
    {
        return await ListAsync<TeamEntity>(ScriptsSql.LIST_ALL_TEAMS);
    }
    
    public async Task<TeamEntity> GetTeamAsync(int teamId)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@team_id", teamId, DbType.Int32);
        return await GetAsync<TeamEntity>(ScriptsSql.GET_TEAM, paramList);
    }
    
    public async Task<int> InsertTeamAsync(TeamEntity team)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@team_name", team.Name, DbType.AnsiString);
        paramList.Add("@team_city", team.City, DbType.AnsiString);
        paramList.Add("@team_short_name", team.ShortName, DbType.AnsiString);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT_TEAM, paramList);
    }

    public async Task UpdateTeamAsync(TeamEntity team)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@team_id", team.Id, DbType.Int32);
        paramList.Add("@team_name", team.Name, DbType.AnsiString);
        paramList.Add("@team_city", team.City, DbType.AnsiString);
        paramList.Add("@team_short_name", team.ShortName, DbType.AnsiString);

        await ExecuteAsync(ScriptsSql.UPDATE_TEAM, paramList);
    }
    
    public async Task DeleteTeamAsync(int teamId)
    {
        
        var paramList = new DynamicParameters();
        paramList.Add("@team_id", teamId, DbType.Int32);
        await ExecuteAsync(ScriptsSql.DELETE_TEAM, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL_TEAMS = @"
                SELECT t.team_id as 'Id'
                      ,t.team_name as 'Name'
                      ,t.team_city as 'City'
	                  ,t.team_short_name as 'ShortName'
                  FROM dbo.team t";

        public static readonly string GET_TEAM = LIST_ALL_TEAMS + @"
                WHERE t.team_id = @team_id";

        public static readonly string INSERT_TEAM = @"
            INSERT
              [dbo].[team] (
                [team_name],
                [team_city],
                [team_short_name]
              )
            VALUES
              (
                @team_name,
                @team_city,
                @team_short_name
              )
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE_TEAM = @"
            UPDATE [dbo].[team]
               SET [team_name] = @team_name
                  ,[team_short_name] = @team_short_name
                  ,[team_city] = @team_city
             WHERE [team_id] = @team_id";
        
        public static readonly string DELETE_TEAM = @"
            DELETE FROM [dbo].[team] WHERE [team_id] = @team_id";
    }
}