using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Entities.Exceptions;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;
using System.Text.Json;

namespace ECA.CBF.Demo.Process
{
    public class MatchEventsProcess : IMatchEventsProcess
    {
        private readonly IMatchDbRepository _matchRepository;
        private readonly IGoalDbRepository _goalRepository;
        private readonly ICardDbRepository _cardRepository;
        private readonly IReplacementDbRepository _replacementRepository;
        private readonly IRabbitMQRepository _rabbitMQRepository;

        public MatchEventsProcess(IMatchDbRepository matchRepository, IGoalDbRepository goalRepository, ICardDbRepository cardRepository, IReplacementDbRepository replacementRepository, IRabbitMQRepository rabbitMQRepository)
        {
            _matchRepository = matchRepository;
            _goalRepository = goalRepository;
            _cardRepository = cardRepository;
            _replacementRepository = replacementRepository;
            _rabbitMQRepository = rabbitMQRepository;
        }

        public async Task SetStartAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetStartAsync(tournmentId, matchId, dateStart);

            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }

            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_START, GetMatchEventMessage(ActionsConstants.MATCH_START, tournmentId, matchId, JsonSerializer.Serialize(dateStart)));
        }
                
        public async Task SetEndAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetEndAsync(tournmentId, matchId, dateStart);

            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_START, GetMatchEventMessage(ActionsConstants.MATCH_END, tournmentId, matchId, JsonSerializer.Serialize(dateStart)));
        }
        public async Task SetStartBreakAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetStartBreakAsync(tournmentId, matchId, dateStart);

            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_BREAK, GetMatchEventMessage(ActionsConstants.MATCH_BREAK, tournmentId, matchId, JsonSerializer.Serialize(dateStart)));
        }
        
        public async Task SetEndBreakAsync(int tournmentId, int matchId, DateTime? dateStart)
        {
            var result = await _matchRepository.SetEndBreakAsync(tournmentId, matchId, dateStart);

            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_BREAK, GetMatchEventMessage(ActionsConstants.MATCH_BREAK, tournmentId, matchId, JsonSerializer.Serialize(dateStart)));
        }

        public async Task<IEnumerable<GoalEntity>> ListGoalsAsync(int id)
        {
            return await _goalRepository.ListAsync(id);
        }


        public async Task<int> InsertGoalsAsync(int tournmentId, int matchId, GoalEntity entity)
        {
            var result = await _goalRepository.InsertAsync(entity);
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_GOAL, GetMatchEventMessage(ActionsConstants.MATCH_GOAL, tournmentId, matchId, JsonSerializer.Serialize(entity)));           
            return result;
        }

        public async Task UpdateGoalsAsync(int tournmentId, int matchId, GoalEntity entity)
        {
            var result = await _goalRepository.UpdateAsync(entity);

            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_GOAL, GetMatchEventMessage(ActionsConstants.MATCH_GOAL_UPDATE, tournmentId, matchId, JsonSerializer.Serialize(entity)));
        }

        public async Task DeleteGoalAsync(int tournmentId, int matchId, int id)
        {
            var result = await _goalRepository.DeleteAsync(id);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_GOAL, GetMatchEventMessage(ActionsConstants.MATCH_GOAL_DELETE, tournmentId, matchId, JsonSerializer.Serialize(id)));
        }

        public async Task<IEnumerable<CardEntity>> ListCardAsync(int id)
        {
            return await _cardRepository.ListAsync(id);
        }

        public async Task<int> InsertCardAsync(int tournmentId, int matchId, CardEntity entity)
        {
            var result = await _cardRepository.InsertAsync(entity);
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_CARD, GetMatchEventMessage(ActionsConstants.MATCH_CARD, tournmentId, matchId, JsonSerializer.Serialize(entity)));
            return result;
        }

        public async Task<int> UpdateCardAsync(int tournmentId, int matchId, CardEntity entity)
        {
            var result = await _cardRepository.UpdateAsync(entity);

            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_CARD, GetMatchEventMessage(ActionsConstants.MATCH_CARD_UPDATE, tournmentId, matchId, JsonSerializer.Serialize(entity)));
            return result;
        }

        public async Task<int> DeleteCardAsync(int tournmentId, int matchId, int id)
        {
            var result = await _cardRepository.DeleteAsync(id);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_CARD, GetMatchEventMessage(ActionsConstants.MATCH_CARD_UPDATE, tournmentId, matchId, JsonSerializer.Serialize(id)));
            return result;
        }

        public async Task<IEnumerable<ReplacementEntity>> ListReplacementAsync(int id)
        {
            return await _replacementRepository.ListAsync(id);
        }

        public async Task<int> InsertReplacementAsync(int tournmentId, int matchId, ReplacementEntity entity)
        {
            var result = await _replacementRepository.InsertAsync(entity);
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_REPLACEMENT, GetMatchEventMessage(ActionsConstants.MATCH_REPLACEMENT, tournmentId, matchId, JsonSerializer.Serialize(entity)));
            return result;
        }

        public async Task<int> UpdateReplacementAsync(int tournmentId, int matchId, ReplacementEntity entity)
        {
            var result = await _replacementRepository.UpdateAsync(entity);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_REPLACEMENT, GetMatchEventMessage(ActionsConstants.MATCH_REPLACEMENT_UPDATE, tournmentId, matchId, JsonSerializer.Serialize(entity)));
            return result;
        }

        public async Task<int> DeleteReplacementAsync(int tournmentId, int matchId, int id)
        {
            var result = await _replacementRepository.DeleteAsync(id);
            if (result == 0)
            {
                throw new ResourceNotFoundException();
            }
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_REPLACEMENT, GetMatchEventMessage(ActionsConstants.MATCH_REPLACEMENT_DELETE, tournmentId, matchId, JsonSerializer.Serialize(id)));
            return result;
        }

        private string GetMatchEventMessage(string action, int tournmentId, int matchId, string content)
        {
            return $"{action}: MatchId: {matchId}, TournmentId: {tournmentId}; content: {content}";
        }

        private static class ActionsConstants
        {
            public static readonly string MATCH_START = "Match start";
            public static readonly string MATCH_END = "Match end";
            public static readonly string MATCH_BREAK = "Break event";
            public static readonly string MATCH_CARD = "New card";
            public static readonly string MATCH_CARD_UPDATE = "Update card";
            public static readonly string MATCH_CARD_DELETE = "Delete card";
            public static readonly string MATCH_GOAL = "New goal";
            public static readonly string MATCH_GOAL_UPDATE = "Update goal";
            public static readonly string MATCH_GOAL_DELETE = "Delete goal";
            public static readonly string MATCH_REPLACEMENT = "New replacement";
            public static readonly string MATCH_REPLACEMENT_UPDATE = "Update replacement";
            public static readonly string MATCH_REPLACEMENT_DELETE = "Delete replacement";

            public static readonly string QUEUE_START = "cbf-match-start";
            public static readonly string QUEUE_BREAK = "cbf-match-break";
            public static readonly string QUEUE_CARD = "cbf-match-card";
            public static readonly string QUEUE_GOAL = "cbf-match-goal";
            public static readonly string QUEUE_REPLACEMENT = "cbf-match-replacement";
        }
    }
}