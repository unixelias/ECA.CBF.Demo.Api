using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface ITournmentProcess
    {
        Task<IEnumerable<TournmentEntity>> ListAsync();

        Task<TournmentEntity> GetAsync(int id);

        Task<int> InsertAsync(TournmentEntity entity);

        Task UpdateAsync(TournmentEntity entity);

        Task DeleteAsync(int id);
    }
}