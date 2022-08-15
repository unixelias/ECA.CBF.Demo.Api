using ECA.CBF.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Repository.Interface
{
    public interface IMatchDbRepository
    {
        Task<IEnumerable<MatchEntity>> ListAsync();

        Task<DateTime?> GetStartAsync(int tournmentId, int matchId);

        Task<int> SetStartAsync(int tournmentId, int matchId, DateTime? dateStart);

        Task<int> SetEndAsync(int tournmentId, int matchId, DateTime? dateEnd);

        Task<int> SetStartBreakAsync(int tournmentId, int matchId, DateTime? dateStartBreak);

        Task<int> SetEndBreakAsync(int tournmentId, int matchId, DateTime? dateEndBreak);

        Task<MatchEntity> GetAsync(int id);

        Task<int> InsertAsync(MatchEntity entity);

        Task UpdateAsync(MatchEntity entity);

        Task DeleteAsync(int id);
    }
}