namespace NineRecommendations.Core.Questionnaires.Answers
{
    public sealed class DefaultLastAnswer : ILastAnswer
    {
        public Guid Id { get; }

        public string Content { get; }

        public DefaultLastAnswer(Guid id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
