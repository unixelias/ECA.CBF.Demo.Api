using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Interface
{
    public interface ITransferDbRepository
    {
        Task<IEnumerable<TransferEntity>> ListAsync();

        Task<TransferEntity> GetAsync(int transferId);

        Task<int> InsertAsync(TransferEntity transfer);

        Task UpdateAsync(TransferEntity transfer);

        Task DeleteAsync(int transferId);
    }
}