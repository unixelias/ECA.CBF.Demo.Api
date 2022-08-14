using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Db;

public class TeamBdRepository : DbBaseRepositoryAsync, ITeamDbRepository
{
    public async Task<IEnumerable<TeamEntity>> ListTeamsAsync()
    {
        return await ListAsync<TeamEntity>(ScriptsSql.LIST_ALL_TEAMS);

    }

    private static class ScriptsSql
    {
        public static readonly string LIST_ALL_TEAMS = @"
                SELECT t.team_id as 'Id'
                      ,t.team_name as 'Name'
                      ,t.team_localidade as 'Locale'
	                  ,l.league_description as 'League'
                  FROM dbo.team t
                  INNER JOIN dbo.league l on t.team_league_id = l.league_id";
    }
}