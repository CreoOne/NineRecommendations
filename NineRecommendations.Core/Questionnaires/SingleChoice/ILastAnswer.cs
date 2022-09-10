using NineRecommendations.Core.Recommendations;

namespace NineRecommendations.Core.Questionnaires.SingleChoice
{
    public interface ILastAnswer : IAnswer
    {
        IRecommendation GetRecommendation();
    }
}
