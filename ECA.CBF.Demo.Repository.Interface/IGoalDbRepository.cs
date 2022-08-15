using ECA.CBF.Demo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Interface
{
    public interface IGoalDbRepository
    {
        Task<IEnumerable<GoalEntity>> ListAsync(int id);

        Task<int> InsertAsync(GoalEntity entity);

        Task<int> UpdateAsync(GoalEntity entity);

        Task<int> DeleteAsync(int id);
    }
}