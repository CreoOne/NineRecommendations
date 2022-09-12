using NineRecommendations.Core.Questionnaires.Questions;

namespace NineRecommendations.Core.Questionnaires.Answers
{
    public sealed class DefaultPassTroughAnswer : IPassTroughAnswer
    {
        public Guid Id { get; }

        public string Content { get; }

        private IQuestion NextQuestion { get; }

        public DefaultPassTroughAnswer(Guid id, string content, IQuestion nextQuestion)
        {
            Id = id;
            Content = content;
            NextQuestion = nextQuestion;
        }

        public IQuestion GetNextQuestion() => NextQuestion;
    }
}
