using NineRecommendations.Core.Questionnaires.Answers;

namespace NineRecommendations.Core.Questionnaires.Questions
{
    public sealed class DefaultQuestion : IQuestion
    {
        public Guid Id { get; }

        public string Content { get;  }

        public IEnumerable<IAnswer> PossibleAnswers { get; }

        public DefaultQuestion(Guid id, string content, IEnumerable<IAnswer> possibleAnswers)
        {
            Id = id;
            Content = content;
            PossibleAnswers = possibleAnswers;
        }
    }
}
