using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;

namespace ECA.CBF.Demo.Process
{
    public class PlayerProcess : IPlayerProcess
    {
        private readonly IPlayerDbRepository _playerRepository;

        public PlayerProcess(IPlayerDbRepository teamRepository)
        {
            _playerRepository = teamRepository;
        }

        public async Task<IEnumerable<PlayerEntity>> ListPlayerAsync()
        {
            List<PlayerEntity> result = new();
            result.AddNonNullOrEmptyRange(await _playerRepository.ListPlayerAsync());
            return result;
        }

        public async Task<PlayerEntity> GetPlayerAsync(int teamId)
        {
            return await _playerRepository.GetPlayerAsync(teamId);
        }

        public async Task<int> InsertPlayerAsync(PlayerEntity team)
        {
            return await _playerRepository.InsertPlayerAsync(team);
        }

        public async Task UpdatePlayerAsync(PlayerEntity team)
        {
            await _playerRepository.UpdatePlayerAsync(team);
        }

        public async Task DeletePlayerAsync(int teamId)
        {
            await _playerRepository.DeletePlayerAsync(teamId);
        }
    }
}