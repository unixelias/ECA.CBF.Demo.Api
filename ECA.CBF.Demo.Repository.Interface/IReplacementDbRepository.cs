using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Interface
{
    public interface IReplacementDbRepository
    {
        Task<IEnumerable<ReplacementEntity>> ListAsync(int id);

        Task<int> InsertAsync(ReplacementEntity entity);

        Task<int> UpdateAsync(ReplacementEntity entity);

        Task<int> DeleteAsync(int id);
    }
}