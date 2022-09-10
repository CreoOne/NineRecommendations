using NineRecommends.Core.Recommendations;

namespace NineRecommends.Core.Questionnaires.SingleChoice
{
    public interface ILastAnswer : IAnswer
    {
        IRecommendation GetRecommendation();
    }
}
