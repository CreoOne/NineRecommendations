using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;

namespace NineRecommendations.Core.Recommendations
{
    public interface IRecommendationBuilder
    {
        IRecommendation? BuildRecommendation(IAnswer answer, IFinder finder, IQuestionnaire questionnaire);
    }
}
