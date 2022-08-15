using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;

namespace ECA.CBF.Demo.Process
{
    public class MatchProcess : IMatchProcess
    {
        private readonly IMatchDbRepository _matchRepository;

        public MatchProcess(IMatchDbRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<IEnumerable<MatchEntity>> ListAsync()
        {
            List<MatchEntity> result = new();
            result.AddNonNullOrEmptyRange(await _matchRepository.ListAsync());
            return result;
        }

        public async Task<MatchEntity> GetAsync(int id)
        {
            return await _matchRepository.GetAsync(id);
        }

        public async Task<int> InsertAsync(MatchEntity entity)
        {
            return await _matchRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(MatchEntity entity)
        {
            await _matchRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _matchRepository.DeleteAsync(id);
        }
    }
}