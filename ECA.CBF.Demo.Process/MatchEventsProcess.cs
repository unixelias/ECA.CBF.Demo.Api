using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Entities.Exceptions;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;

namespace ECA.CBF.Demo.Process
{
    public class MatchEventsProcess : IMatchEventsProcess
    {
        private readonly IMatchDbRepository _matchRepository;
        private readonly IGoalDbRepository _goalRepository;
        private readonly ICardDbRepository _cardRepository;
        private readonly IReplacementDbRepository _replacementRepository;

        public MatchEventsProcess(IMatchDbRepository matchRepository, IGoalDbRepository goalRepository, ICardDbRepository cardRepository, IReplacementDbRepository replacementRepository)
        {
            _matchRepository = matchRepository;
            _goalRepository = goalRepository;
            _cardRepository = cardRepository;
            _replacementRepository = replacementRepository;
        }

        public async Task<DateTime?> GetStartAsync(int tournmentId, int matchId)
        {
            var result = await _matchRepository.GetStartAsync(tournmentId, matchId);
            return result;
        }

        public async Task SetStartAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetStartAsync(tournmentId, matchId, dateStart); if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
        }
        
        public async Task SetEndAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetEndAsync(tournmentId, matchId, dateStart); if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
        }
        public async Task SetStartBreakAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetStartBreakAsync(tournmentId, matchId, dateStart);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
        }
        
        public async Task SetEndBreakAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetEndBreakAsync(tournmentId, matchId, dateStart);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
        }

        public async Task<IEnumerable<GoalEntity>> ListGoalsAsync(int id)
        {
            return await _goalRepository.ListAsync(id);
        }


        public async Task<int> InsertGoalsAsync(GoalEntity entity)
        {
            return await _goalRepository.InsertAsync(entity);
        }

        public async Task UpdateGoalsAsync(GoalEntity entity)
        {
            var result = await _goalRepository.UpdateAsync(entity);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
        }

        public async Task DeleteGoalAsync(int id)
        {
            var result = await _goalRepository.DeleteAsync(id);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
        }

        public async Task<IEnumerable<CardEntity>> ListCardAsync(int id)
        {
            return await _cardRepository.ListAsync(id);
        }

        public async Task<int> InsertCardAsync(CardEntity entity)
        {
            return await _cardRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateCardAsync(CardEntity entity)
        {
            var result = await _cardRepository.UpdateAsync(entity);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            return result;
        }

        public async Task<int> DeleteCardAsync(int id)
        {
            var result = await _cardRepository.DeleteAsync(id);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            return result;
        }

        public async Task<IEnumerable<ReplacementEntity>> ListReplacementAsync(int id)
        {
            return await _replacementRepository.ListAsync(id);
        }

        public async Task<int> InsertReplacementAsync(ReplacementEntity entity)
        {
            return await _replacementRepository.InsertAsync(entity);
        }

        public async Task<int> UpdateReplacementAsync(ReplacementEntity entity)
        {
            var result = await _replacementRepository.UpdateAsync(entity);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            return result;
        }

        public async Task<int> DeleteReplacementAsync(int id)
        {
            var result = await _replacementRepository.DeleteAsync(id);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            return result;
        }
    }
}