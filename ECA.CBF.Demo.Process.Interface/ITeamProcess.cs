using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface ITeamProcess
    {
        Task<IEnumerable<TeamEntity>> ListTeamsAsync();
    }
}