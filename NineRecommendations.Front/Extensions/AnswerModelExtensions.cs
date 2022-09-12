using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Extensions
{
    public static class AnswerModelExtensions
    {
        public static AnswerModel ToViewModel(this IAnswer answer) => new()
        {
            Id = answer.Id,
            Content = answer.Content
        };
    }
}
