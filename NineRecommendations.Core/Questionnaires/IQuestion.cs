namespace NineRecommendations.Core.Questionnaires
{
    public interface IQuestion
    {
        Guid Id { get; }
        string Content { get; }
        IEnumerable<IAnswer> PossibleAnswers { get; }
    }
}
