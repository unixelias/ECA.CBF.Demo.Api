using ECA.CBF.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Process.Interface
{
    public interface IMatchEventsProcess
    {
        Task<DateTime?> GetStartAsync(int tournmentId, int matchId);

        Task SetStartAsync(int tournmentId, int matchId, DateTime? dateStart);

        Task SetEndAsync(int tournmentId, int matchId, DateTime? dateEnd);

        Task SetStartBreakAsync(int tournmentId, int matchId, DateTime? dateStartBreak);

        Task SetEndBreakAsync(int tournmentId, int matchId, DateTime? dateEndBreak);

        Task<IEnumerable<GoalEntity>> ListGoalsAsync(int id);

        Task<int> InsertGoalsAsync(GoalEntity entity);

        Task UpdateGoalsAsync(GoalEntity entity);

        Task DeleteGoalAsync(int id);

        Task<IEnumerable<CardEntity>> ListCardAsync(int id);

        Task<int> InsertCardAsync(CardEntity entity);

        Task<int> UpdateCardAsync(CardEntity entity);

        Task<int> DeleteCardAsync(int id);

        Task<IEnumerable<ReplacementEntity>> ListReplacementAsync(int id);

        Task<int> InsertReplacementAsync(ReplacementEntity entity);

        Task<int> UpdateReplacementAsync(ReplacementEntity entity);

        Task<int> DeleteReplacementAsync(int id);
    }
}