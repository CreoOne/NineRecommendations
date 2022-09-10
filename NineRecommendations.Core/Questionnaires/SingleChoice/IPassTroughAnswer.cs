namespace NineRecommendations.Core.Questionnaires.SingleChoice
{
    public interface IPassTroughAnswer : IAnswer
    {
        IQuestion GetNextQuestion();
    }
}
