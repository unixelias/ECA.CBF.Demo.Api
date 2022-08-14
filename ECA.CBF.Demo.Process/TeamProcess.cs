using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Db;
using ECA.CBF.Demo.Repository.Interface;

namespace ECA.CBF.Demo.Process
{
    public class TeamProcess : ITeamProcess
    {
        private readonly ITeamDbRepository _teamRepository;


        public TeamProcess(ITeamDbRepository teamRepository)
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