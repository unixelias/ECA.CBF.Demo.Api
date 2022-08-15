using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface IMatchProcess
    {
        Task<IEnumerable<MatchEntity>> ListAsync();

        Task<MatchEntity> GetAsync(int id);

        Task<int> InsertAsync(MatchEntity entity);

        Task UpdateAsync(MatchEntity entity);

        Task DeleteAsync(int id);
    }
}