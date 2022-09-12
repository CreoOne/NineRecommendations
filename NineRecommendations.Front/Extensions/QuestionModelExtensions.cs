using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Extensions
{
    public static class QuestionModelExtensions
    {
        public static QuestionModel ToViewModel(this IQuestion question) => new()
        {
            Content = question.Content,
            Id = question.Id,
            Answers = question.PossibleAnswers.Select(answer => answer.ToViewModel())
        };
    }
}
