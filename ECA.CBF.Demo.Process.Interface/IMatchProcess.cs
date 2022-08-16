using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface IMatchProcess
    {
        Task<IEnumerable<MatchExtendedEntity>> ListAsync();

        Task<MatchExtendedEntity> GetAsync(int id);

        Task<int> InsertAsync(MatchBaseEntity entity);

        Task UpdateAsync(MatchBaseEntity entity);

        Task DeleteAsync(int id);
    }
}