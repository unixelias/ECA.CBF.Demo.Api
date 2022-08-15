using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class TournmentDbRepository : DbBaseRepositoryAsync, ITournmentDbRepository
{
    public async Task<IEnumerable<TournmentEntity>> ListAsync()
    {
        return await ListAsync<TournmentEntity>(ScriptsSql.LIST_ALL);
    }
    
    public async Task<TournmentEntity> GetAsync(int id)
    {
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", id, DbType.Int32);
        return await GetAsync<TournmentEntity>(ScriptsSql.GET, paramList);
    }
    
    public async Task<int> InsertAsync(TournmentEntity entity)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@tournament_name", entity.Name, DbType.AnsiString);
        paramList.Add("@tournament_grade", entity.Grade, DbType.AnsiString);
        paramList.Add("@tournament_season", entity.Season, DbType.Int64);
        paramList.Add("@tournament_league_id", entity.LeagueId, DbType.Int32);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT, paramList);
    }

    public async Task UpdateAsync(TournmentEntity entity)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@tournament_id", entity.Id, DbType.Int32);
        paramList.Add("@tournament_name", entity.Name, DbType.AnsiString);
        paramList.Add("@tournament_grade", entity.Grade, DbType.AnsiString);
        paramList.Add("@tournament_season", entity.Season, DbType.Int32);
        paramList.Add("@tournament_league_id", entity.LeagueId, DbType.Int32);

        await ExecuteAsync(ScriptsSql.UPDATE, paramList);
    }
    
    public async Task DeleteAsync(int id)
    {
        
        var paramList = new DynamicParameters();
        paramList.Add("@tournament_id", id, DbType.Int32);
        await ExecuteAsync(ScriptsSql.DELETE, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL = @"
            SELECT [t].[tournament_id] AS Id
                    ,[t].[tournament_name] AS 'Name'
                    ,[t].[tournament_grade] AS Grade
                    ,[t].[tournament_season] AS Season
                    ,[t].[tournament_league_id] AS LeagueId
	                ,[l].[league_description] AS League
                FROM [dbo].[tournament] [t]
                JOIN [dbo].[league] [l] ON [t].[tournament_league_id] = [l].[league_id]";

        public static readonly string GET = LIST_ALL + @"
                WHERE [t].[tournament_id] = @tournament_id";

        public static readonly string INSERT = @"
        INSERT INTO [dbo].[tournament]
                   ([tournament_name]
                   ,[tournament_grade]
                   ,[tournament_season]
                   ,[tournament_league_id])
             VALUES
                   (@tournament_name
                   ,@tournament_grade
                   ,@tournament_season
                   ,@tournament_league_id)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE = @"
            UPDATE [dbo].[tournament]
               SET [tournament_name] = @tournament_name
                  ,[tournament_grade] = @tournament_grade
                  ,[tournament_season] = @tournament_season
                  ,[tournament_league_id] = @tournament_league_id
             WHERE [tournament_id] = @tournament_id";

        public static readonly string DELETE = @"
            DELETE FROM [dbo].[tournament] WHERE [tournament_id] = @tournament_id";
    }
}