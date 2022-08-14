using ECA.CBF.Demo.Entities;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface ITeamProcess
    {
        Task<IEnumerable<TeamEntity>> ListTeamsAsync();
    }
}