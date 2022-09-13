using NineRecommendations.Core.Questionnaires.Primitives;
using NineRecommendations.Core.Questionnaires.Questions;

namespace NineRecommendations.Core.Questionnaires
{
    public interface IQuestionnaireManipulator
    {
        IQuestion? GetQuestionById(Guid questionId);
        Task<AnswerProcessingResult> ProcessAnswerAsync(Guid questionnaireId, Guid questionId, Guid answerId);
        Task<Guid> StartNewQuestionnaireAsync();
        Guid GetFirstQuestionId();
    }
}