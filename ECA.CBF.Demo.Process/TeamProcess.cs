using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Db;

namespace ECA.CBF.Demo.Process
{
    public class TeamProcess : ITeamProcess
    {
        private readonly TeamBdRepository _teamRepository;


        public TeamProcess(TeamBdRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }


        public async Task<IEnumerable<TeamEntity>> ListTeamsAsync()
        {
            var result = await _teamRepository.ListTeamsAsync();
            return result;
        }
    }
}