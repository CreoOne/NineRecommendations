using NineRecommendations.Core.Recommendations;

namespace NineRecommendations.Core.Questionnaires
{
    public interface ILastAnswer : IAnswer
    {
        IRecommendation GetRecommendation(IQuestionnaire questionnaire);
    }
}
