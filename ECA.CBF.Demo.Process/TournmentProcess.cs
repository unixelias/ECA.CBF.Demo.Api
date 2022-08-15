using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;

namespace ECA.CBF.Demo.Process
{
    public class TournmentProcess : ITournmentProcess
    {
        private readonly ITournmentDbRepository _tournmentRepository;

        public TournmentProcess(ITournmentDbRepository tournmentRepository)
        {
            _tournmentRepository = tournmentRepository;
        }

        public async Task<IEnumerable<TournmentEntity>> ListAsync()
        {
            List<TournmentEntity> result = new();
            result.AddNonNullOrEmptyRange(await _tournmentRepository.ListAsync());
            return result;
        }

        public async Task<TournmentEntity> GetAsync(int id)
        {
            return await _tournmentRepository.GetAsync(id);
        }

        public async Task<int> InsertAsync(TournmentEntity entity)
        {
            return await _tournmentRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(TournmentEntity entity)
        {
            await _tournmentRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _tournmentRepository.DeleteAsync(id);
        }
    }
}