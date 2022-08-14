using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface ITeamProcess
    {
        Task<IEnumerable<TeamEntity>> ListTeamsAsync();

        Task<TeamEntity> GetTeamAsync(int teamId);

        Task<int> InsertTeamAsync(TeamEntity team);

        Task UpdateTeamAsync(TeamEntity team);
    }
}