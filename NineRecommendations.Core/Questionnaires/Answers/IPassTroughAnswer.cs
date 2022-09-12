using NineRecommendations.Core.Questionnaires.Questions;

namespace NineRecommendations.Core.Questionnaires.Answers
{
    public interface IPassTroughAnswer : IAnswer
    {
        IQuestion GetNextQuestion();
    }
}
