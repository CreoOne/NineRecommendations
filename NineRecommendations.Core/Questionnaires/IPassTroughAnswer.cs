namespace NineRecommendations.Core.Questionnaires
{
    public interface IPassTroughAnswer : IAnswer
    {
        IQuestion GetNextQuestion();
    }
}
