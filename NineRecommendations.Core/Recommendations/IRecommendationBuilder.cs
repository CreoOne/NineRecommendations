using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Core.Recommendations
{
    public interface IRecommendationBuilder
    {
        IRecommendation? BuildRecommendation(IAnswer answer, IQuestionnaire questionnaire);
    }
}
