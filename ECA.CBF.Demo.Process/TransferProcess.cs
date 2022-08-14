using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;

namespace ECA.CBF.Demo.Process
{
    public class TransferProcess : ITransferProcess
    {
        private readonly ITransferDbRepository _teamRepository;

        public TransferProcess(ITransferDbRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<TransferEntity>> ListAsync()
        {
            List<TransferEntity> result = new();
            result.AddNonNullOrEmptyRange(await _teamRepository.ListAsync());
            return result;
        }

        public async Task<TransferEntity> GetAsync(int transferId)
        {
            return await _teamRepository.GetAsync(transferId);
        }

        public async Task<int> InsertAsync(TransferEntity transfer)
        {
            return await _teamRepository.InsertAsync(transfer);
        }

        public async Task UpdateAsync(TransferEntity transfer)
        {
            await _teamRepository.UpdateAsync(transfer);
        }

        public async Task DeleteAsync(int transferId)
        {
            await _teamRepository.DeleteAsync(transferId);
        }
    }
}