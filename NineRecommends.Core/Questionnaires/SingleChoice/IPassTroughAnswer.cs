namespace NineRecommends.Core.Questionnaires.SingleChoice
{
    public interface IPassTroughAnswer : IAnswer
    {
        IQuestion GetNextQuestion();
    }
}
