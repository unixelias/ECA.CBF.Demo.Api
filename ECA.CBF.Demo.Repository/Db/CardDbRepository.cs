using Dapper;
using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class CardDbRepository : DbBaseRepositoryAsync, ICardDbRepository
{
    public async Task<IEnumerable<CardEntity>> ListAsync(int id)
    {

        var paramList = new DynamicParameters();
        paramList.Add("@card_match_id", id, DbType.Int32);
        return await ListAsync<CardEntity>(ScriptsSql.GET_BY_MATCH, paramList);
    }
    
    public async Task<int> InsertAsync(CardEntity entity)
    {

        var paramList = new DynamicParameters();

        paramList.Add("@card_type", entity.Type, DbType.AnsiString);
        paramList.Add("@card_description", entity.Description, DbType.AnsiString);
        paramList.Add("@card_match_id", entity.MatchId, DbType.Int32);
        paramList.Add("@card_player_id", entity.PlayerId, DbType.Int32);
        paramList.Add("@card_team_id", entity.TeamId, DbType.Int32);
        paramList.Add("@card_date_time", entity.Date, DbType.DateTime);

        return await ExecuteAndReturnAsync<int>(ScriptsSql.INSERT, paramList);
    }

    public async Task<int> UpdateAsync(CardEntity entity)
    {
        
        var paramList = new DynamicParameters();

        paramList.Add("@card_id", entity.Id, DbType.Int32);
        paramList.Add("@card_type", entity.Type, DbType.AnsiString);
        paramList.Add("@card_description", entity.Description, DbType.AnsiString);
        paramList.Add("@card_match_id", entity.MatchId, DbType.Int32);
        paramList.Add("@card_player_id", entity.PlayerId, DbType.Int32);
        paramList.Add("@card_team_id", entity.TeamId, DbType.Int32);
        paramList.Add("@card_date_time", entity.Date, DbType.DateTime);

        return await ExecuteAsync(ScriptsSql.UPDATE, paramList);
    }
    
    public async Task<int> DeleteAsync(int id)
    {        
        var paramList = new DynamicParameters();
        paramList.Add("@card_id", id, DbType.Int32);
        return await ExecuteAsync(ScriptsSql.DELETE, paramList);
    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL = @"
            SELECT [c].[card_id] AS Id
                  ,[c].[card_type] AS Type
                  ,[c].[card_description] AS Description
                  ,[c].[card_match_id] AS MatchId
                  ,[c].[card_player_id] AS PlayerId
	              ,[p].[player_name] AS PlayerName
                  ,[c].[card_team_id] AS TeamId
	              ,[t].[team_name] AS TeamName
                  ,[c].[card_date_time] AS Date
              FROM [dbo].[card] [c]
              INNER JOIN [dbo].[team] [t] ON [c].[card_team_id] = [t].[team_id]
              INNER JOIN [dbo].[player] [p] ON [c].[card_player_id] = [p].[player_id]";

        public static readonly string GET_BY_MATCH = LIST_ALL + @"
                WHERE [c].[card_match_id] = @card_match_id";

        public static readonly string INSERT = @"
            INSERT INTO [dbo].[card]
                       ([card_type]
                       ,[card_description]
                       ,[card_match_id]
                       ,[card_player_id]
                       ,[card_team_id]
                       ,[card_date_time])
                 VALUES
                       (@card_type
                       ,@card_description
                       ,@card_match_id
                       ,@card_player_id
                       ,@card_team_id
                       ,@card_date_time)
            SELECT SCOPE_IDENTITY()";

        public static readonly string UPDATE = @"
            UPDATE [dbo].[card]
               SET [card_type] = @card_type
                  ,[card_description] = @card_description
                  ,[card_match_id] = @card_match_id
                  ,[card_player_id] = @card_player_id
                  ,[card_team_id] = @card_team_id
                  ,[card_date_time] = @card_date_time
             WHERE [card_id] = @card_id";

        public static readonly string DELETE = @"
            DELETE FROM [dbo].[card] WHERE [card_id] = @card_id";
    }
}