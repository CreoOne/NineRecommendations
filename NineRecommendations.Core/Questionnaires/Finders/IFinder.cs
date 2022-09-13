using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Questions;

namespace NineRecommendations.Core.Questionnaires.Finders
{
    public interface IFinder
    {
        IQuestion? FindQuestionById(Guid id);
        IAnswer? FindAnswerById(Guid id);
        IQuestion GetFirstQuestion();
        IQuestion GetRefineQuestion();
    }
}
