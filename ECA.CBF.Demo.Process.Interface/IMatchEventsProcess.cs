using ECA.CBF.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface IMatchEventsProcess
    {
        Task SetStartAsync(int tournmentId, int matchId, DateTime? dateStart);

        Task SetEndAsync(int tournmentId, int matchId, DateTime? dateEnd);

        Task SetStartBreakAsync(int tournmentId, int matchId, DateTime? dateStartBreak);

        Task SetEndBreakAsync(int tournmentId, int matchId, DateTime? dateEndBreak);

        Task<IEnumerable<GoalEntity>> ListGoalsAsync(int id);

        Task<int> InsertGoalsAsync(int tournmentId, int matchId, GoalEntity entity);

        Task UpdateGoalsAsync(int tournmentId, int matchId, GoalEntity entity);

        Task DeleteGoalAsync(int tournmentId, int matchId, int id);

        Task<IEnumerable<CardEntity>> ListCardAsync(int id);

        Task<int> InsertCardAsync(int tournmentId, int matchId, CardEntity entity);

        Task<int> UpdateCardAsync(int tournmentId, int matchId, CardEntity entity);

        Task<int> DeleteCardAsync(int tournmentId, int matchId, int id);

        Task<IEnumerable<ReplacementEntity>> ListReplacementAsync(int id);

        Task<int> InsertReplacementAsync(int tournmentId, int matchId, ReplacementEntity entity);

        Task<int> UpdateReplacementAsync(int tournmentId, int matchId, ReplacementEntity entity);

        Task<int> DeleteReplacementAsync(int tournmentId, int matchId, int id);
    }
}