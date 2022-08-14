using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Db;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;

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
            List<TeamEntity> result = new();
            result.AddNonNullOrEmptyRange(await _teamRepository.ListTeamsAsync());
            return result;
        }
        public async Task<TeamEntity> GetTeamAsync(int teamId)
        {            
            return await _teamRepository.GetTeamAsync(teamId);
        }


        public async Task<int> InsertTeamAsync(TeamEntity team)
        {
            return await _teamRepository.InsertTeamAsync(team);
        }

        public async Task UpdateTeamAsync(TeamEntity team)
        {
            await _teamRepository.UpdateTeamAsync(team);
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            await _teamRepository.DeleteTeamAsync(teamId);
        }
    }
}