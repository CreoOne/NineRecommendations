using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Core.Persistence
{
    public interface IQuestionnaireRepository
    {
        Task Save(IQuestionnaire questionnaire);
        Task<IQuestionnaire?> Load(Guid id);
    }
}
