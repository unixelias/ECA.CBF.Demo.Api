using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface ITournmentProcess
    {
        Task<IEnumerable<TournmentEntity>> ListAsync();

        Task<TournmentEntity> GetAsync(int transferId);

        Task<int> InsertAsync(TournmentEntity transfer);

        Task UpdateAsync(TournmentEntity transfer);

        Task DeleteAsync(int transferId);
    }
}