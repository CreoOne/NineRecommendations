using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;

namespace NineRecommendations.Core.Recommendations
{
    public interface IRecommendationBuilder
    {
        IRecommendation? BuildRecommendation(IAnswer answer, IQuestionnaire questionnaire);
    }
}
