using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECA.CBF.Demo.Entities
{
    public class MatchExtendedEntity : MatchEntity
    {
        [JsonPropertyName("goal-list")]
        public IEnumerable<GoalEntity> GoalsList { get; set; }

        [JsonPropertyName("card-list")]
        public IEnumerable<CardEntity> CardsList { get; set; }

        [JsonPropertyName("replacement-list")]
        public IEnumerable<ReplacementEntity> ReplacementsList { get; set; }
    }

    public static class MatchExtendedEntityExtension
    {
        public static MatchExtendedEntity GetMatchExtended(this MatchEntity entity, IEnumerable<GoalEntity> goalEntities, IEnumerable<CardEntity> cardEntities, IEnumerable<ReplacementEntity> replacementEntities)
        {
            if (entity == null)
            {
                return default;
            }
            return new MatchExtendedEntity()
            {
                DateEnd = entity.DateEnd,
                DateProvided = entity.DateProvided,
                DateStart = entity.DateStart,
                DateStartBreak = entity.DateStartBreak,
                DateStopBreak = entity.DateStopBreak,
                Id = entity.Id,
                RefereeId = entity.RefereeId,
                RefereeName = entity.RefereeName,
                Stadium = entity.Stadium,
                TeamGuest = entity.TeamGuest,
                TeamGuestGoals = entity.TeamGuestGoals,
                TeamGuestId = entity.TeamGuestId,
                TeamHost = entity.TeamHost,
                TeamHostGoals = entity.TeamHostGoals,
                TeamHostId = entity.TeamHostId,
                Tournment = entity.Tournment,
                TournmentId = entity.TournmentId,
                CardsList = cardEntities,
                GoalsList = goalEntities,
                ReplacementsList = replacementEntities
            };
        }
    }
}