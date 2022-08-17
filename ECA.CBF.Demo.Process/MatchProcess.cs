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

            _rabbitMQRepository.SendMessageToQueue("cbf-match", JsonSerializer.Serialize(match));
            return match.GetMatchExtended(goals, cards, replacements);
        }

        public async Task<int> InsertAsync(MatchBaseEntity entity)
        {
            return await _matchRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(MatchBaseEntity entity)
        {
            await _matchRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _matchRepository.DeleteAsync(id);
        }
    }
}