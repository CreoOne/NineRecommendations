namespace NineRecommendations.Core.Questionnaires.Answers
{
    public interface IAnswer
    {
        Guid Id { get; }
        string Content { get; }
    }
}
