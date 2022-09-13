using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Core.Persistence
{
    public interface IQuestionnaireRepository
    {
        Task SaveAsync(IQuestionnaire questionnaire);
        Task<IQuestionnaire?> LoadAsync(Guid id);
    }
}
