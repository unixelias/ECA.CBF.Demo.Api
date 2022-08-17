using ECA.CBF.Demo.Entities;
using ECA.CBF.Demo.Entities.Exceptions;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Interface;
using ECA.CBF.Demo.Util;
using System.Text.Json;

namespace ECA.CBF.Demo.Process
{
    public class MatchProcess : IMatchProcess
    {
        private readonly IMatchDbRepository _matchRepository;
        private readonly IGoalDbRepository _goalRepository;
        private readonly ICardDbRepository _cardRepository;
        private readonly IReplacementDbRepository _replacementRepository;
        private readonly IRabbitMQRepository _rabbitMQRepository;

        public MatchProcess(IMatchDbRepository matchRepository, IGoalDbRepository goalRepository, ICardDbRepository cardRepository, IReplacementDbRepository replacementRepository, IRabbitMQRepository rabbitMQRepository)
        {
            _matchRepository = matchRepository;
            _goalRepository = goalRepository;
            _cardRepository = cardRepository;
            _replacementRepository = replacementRepository;
            _rabbitMQRepository = rabbitMQRepository;
        }

        public async Task<IEnumerable<MatchExtendedEntity>> ListAsync()
        {
            List<MatchEntity> matchList = new();
            List<MatchExtendedEntity> matchExtendedList = new();

            matchList.AddNonNullOrEmptyRange(await _matchRepository.ListAsync());
            if (matchList.Any())
            {
                foreach (var match in matchList)
                {
                    var goals = await _goalRepository.ListAsync(match.Id);
                    var cards = await _cardRepository.ListAsync(match.Id);
                    var replacements = await _replacementRepository.ListAsync(match.Id);
                    matchExtendedList.Add(match.GetMatchExtended(goals, cards, replacements));
                }
            }

            return matchExtendedList;
        }

        public async Task<MatchExtendedEntity> GetAsync(int id)
        {
            var match = await _matchRepository.GetAsync(id);

            if (match == null)
            {
                throw new ResourceNotFoundException("Match does not exists");
            }

            var goals = await _goalRepository.ListAsync(match.Id);
            var cards = await _cardRepository.ListAsync(match.Id);
            var replacements = await _replacementRepository.ListAsync(match.Id);

            return match.GetMatchExtended(goals, cards, replacements);
        }

        public async Task<int> InsertAsync(MatchBaseEntity entity)
        {
            var result = await _matchRepository.InsertAsync(entity);
            var match = await GetAsync(result);

            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_MATCH, GetMatchEventMessage(ActionsConstants.MATCH_NEW, match.TournmentId, match.Id, JsonSerializer.Serialize(match)));
            return result;
        }

        public async Task UpdateAsync(MatchBaseEntity entity)
        {
            await _matchRepository.UpdateAsync(entity);
            var match = await GetAsync(entity.Id);
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_MATCH, GetMatchEventMessage(ActionsConstants.MATCH_UPDATE, match.TournmentId, match.Id, JsonSerializer.Serialize(match)));
        }

        public async Task DeleteAsync(int id)
        {
            await _matchRepository.DeleteAsync(id);
            _rabbitMQRepository.SendMessageToQueue(ActionsConstants.QUEUE_MATCH, GetMatchEventMessage(ActionsConstants.MATCH_DELETE, default, id, string.Empty));
        }

        private string GetMatchEventMessage(string action, int tournmentId, int matchId, string content)
        {
            return $"{action}: MatchId: {matchId}, TournmentId: {tournmentId}; content: {content}";
        }

        private static class ActionsConstants
        {
            public static readonly string MATCH_NEW = "Match created";
            public static readonly string MATCH_UPDATE = "Match updated";
            public static readonly string MATCH_DELETE = "Break deleted";

            public static readonly string QUEUE_MATCH = "cbf-match";
        }
    }
}