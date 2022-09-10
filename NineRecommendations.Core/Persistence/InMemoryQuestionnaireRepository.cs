using NineRecommendations.Core.Questionnaires;
using System.Collections.Concurrent;

namespace NineRecommendations.Core.Persistence
{
    public sealed class InMemoryQuestionnaireRepository : IQuestionnaireRepository
    {
        private ConcurrentDictionary<Guid, IQuestionnaire> Questionnaires { get; } = new();

        public Task<IQuestionnaire?> Load(Guid id)
        {
            Questionnaires.TryGetValue(id, out var questionnaire);
            return Task.FromResult(questionnaire);
        }

        public Task Save(IQuestionnaire questionnaire)
        {
            Questionnaires.TryAdd(questionnaire.Id, questionnaire);
            return Task.CompletedTask;
        }
    }
}
