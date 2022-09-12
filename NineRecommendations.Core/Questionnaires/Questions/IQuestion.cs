using NineRecommendations.Core.Questionnaires.Answers;

namespace NineRecommendations.Core.Questionnaires.Questions
{
    public interface IQuestion
    {
        Guid Id { get; }
        string Content { get; }
        IEnumerable<IAnswer> PossibleAnswers { get; }
    }
}
